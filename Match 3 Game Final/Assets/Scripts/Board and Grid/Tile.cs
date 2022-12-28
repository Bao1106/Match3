using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Tile : MonoBehaviour {
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static Tile previousSelected = null;

	private SpriteRenderer render;
	private bool isSelected = false;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
	private List<GameObject> adjacentTiles;

	void Awake() {
		render = GetComponent<SpriteRenderer>();
    }

	private void Select() {
		isSelected = true;
		render.color = selectedColor;
		previousSelected = gameObject.GetComponent<Tile>();
		SFXManager.instance.PlaySFX(Clip.Select);
	}

	private void Deselect() {
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}

	void OnMouseDown() {
		if (render.sprite == null || BoardManager.instance.IsShifting) {
			return;
		}

		if (isSelected) {
			Deselect();
		} else {
			if (previousSelected == null) { 
				Select();
			} else {
				if (GetAllAdjacentTiles().Contains(previousSelected.gameObject)) { 
					SwapSprite(previousSelected.render);
					previousSelected.ClearAllMatches();
					previousSelected.Deselect();
					ClearAllMatches();
				} else {
					previousSelected.GetComponent<Tile>().Deselect();
					Select();
				}
			}
		}
	}

	public void SwapSprite(SpriteRenderer render2) {

		Vector2 swapTempPos = render.gameObject.transform.position;
		Vector2 pressTempPos = render2.gameObject.transform.position;

        Vector2 object1 = new Vector2(4f, 4f);
        Vector2 object2 = new Vector2(4f, 5f);
        Vector2 object3 = new Vector2(5f, 4f);
        Vector2 object4 = new Vector2(5f, 5f);

        bool checkPos = BoardManager.instance.blackTile.Any(_ => _.Equals(swapTempPos)) 
			|| BoardManager.instance.blackTile.Any(_ => _.Equals(pressTempPos));

		Debug.LogError($"swapTempPos: {swapTempPos}");
		Debug.LogError($"checkPos: {checkPos}");

		if (render.sprite == render2.sprite) 
		{
			return;
		}

		if (checkPos)
        {
			return;
        }
        else if (swapTempPos == object1 || swapTempPos == object2  || swapTempPos == object3 || swapTempPos == object4)
        {
            return;
        }
        else if (pressTempPos == object1 || pressTempPos == object2 || pressTempPos == object3 || pressTempPos == object4)
        {
            return;
        }
        else
        {
			Sprite tempSprite = render2.sprite;
			render2.sprite = render.sprite;
			render.sprite = tempSprite;
			SFXManager.instance.PlaySFX(Clip.Swap);
			//GUIManager.instance.MoveCounter--;
		}
	}

	private GameObject GetAdjacent(Vector2 castDir) {
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir/*, 1f, BoardManager.instance.layerMask*/);
		if (hit.collider != null) {
			return hit.collider.gameObject;
		}
		return null;
	}

	private List<GameObject> GetAllAdjacentTiles() {
		adjacentTiles = new List<GameObject>();
		for (int i = 0; i < adjacentDirections.Length; i++) {
			adjacentTiles.Add(GetAdjacent(adjacentDirections[i]));
		}

        return adjacentTiles;
	}

	private List<GameObject> FindMatch(Vector2 castDir) {
		List<GameObject> matchingTiles = new List<GameObject>();
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir/*, 1f, BoardManager.instance.layerMask*/);
		while (hit.collider != null && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite) {
			matchingTiles.Add(hit.collider.gameObject);
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
		}
		return matchingTiles;
	}

	private void ClearMatch(Vector2[] paths) {
		List<GameObject> matchingTiles = new List<GameObject>();
		for (int i = 0; i < paths.Length; i++) { matchingTiles.AddRange(FindMatch(paths[i])); }
		if (matchingTiles.Count >= 2) {
			for (int i = 0; i < matchingTiles.Count; i++) {
				matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
			}
			matchFound = true;
		}
	}

	private bool matchFound = false;
	public void ClearAllMatches() {
		if (render.sprite == null)
			return;

		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
		if (matchFound) {
			render.sprite = null;
			matchFound = false;
			StopCoroutine(BoardManager.instance.FindNullTiles());
			StartCoroutine(BoardManager.instance.FindNullTiles());
			SFXManager.instance.PlaySFX(Clip.Clear);
		}
	}
}