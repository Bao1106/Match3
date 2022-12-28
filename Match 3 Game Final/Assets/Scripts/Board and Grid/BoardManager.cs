using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public class BoardManager : MonoBehaviour {
	public static BoardManager instance;
	public int xSize, ySize;
	public LayerMask layerMask;
	public List<Vector2> blackTile = new List<Vector2>();

	[SerializeField] private List<Sprite> characters;
	[SerializeField] private GameObject objectTile, boardTile, boardTileContain, blackBoardTile, blackBoardTileContain;

	private GameObject[,] objectTiles, boardTiles;

	public ReactiveProperty<bool> openSettingPopup = new ReactiveProperty<bool>(false);

	public bool IsShifting { get; set; }

    private void Awake()
    {
		instance = this;
	}

    void Start () {
		
		boardTiles = new GameObject[xSize, ySize];
		objectTiles = new GameObject[xSize, ySize];

		//Vector2 offset = objectTile.GetComponent<SpriteRenderer>().bounds.size;
        CreateBoard();
    }

	private void CreateBoard () 
	{
		Sprite[] previousLeft = new Sprite[ySize]; // Add this line
		Sprite previousBelow = null; // Add this line

		int xBlackSize = 3, total = ySize - 1;
		//int xWhiteSize = 

		for(int i = xBlackSize - 2; i >= 0; i--)
        {
			for (int j = 0; j < xBlackSize - 1; j++)
            {
                Vector2 tempPostion = new Vector2(xBlackSize + 1 + i, xBlackSize + 1 + j);
                GameObject boardTileBG = Instantiate(blackBoardTile, tempPostion, Quaternion.identity);

				boardTileBG.name = $"Tile Middle - {xBlackSize + 1 + i} , {xBlackSize + 1 + j}";
				boardTileBG.transform.SetParent(blackBoardTileContain.transform);

				blackTile.Add(tempPostion);
			}
        }

        for (int i = xBlackSize - 1; i >= 0; i--)
        {
            for (int j = 0; j < i + 1; j++)
            {
                Vector2 tempPos = new Vector2(xBlackSize - 1 - i, j);
                GameObject boardTileBG = Instantiate(blackBoardTile, tempPos, Quaternion.identity);
				boardTileBG.name = $"Tile - {xBlackSize - 1 - i} , {j}";

				Vector2 tempPos1 = new Vector2(xBlackSize - 1 - i, total - j);
                GameObject boardTileBG1 = Instantiate(blackBoardTile, tempPos1, Quaternion.identity);
				boardTileBG1.name = $"Tile - {xBlackSize - 1 - i} , {total - j}";

				Vector2 tempPos2 = new Vector2(i + total - xBlackSize + 1, j);
                GameObject boardTileBG2 = Instantiate(blackBoardTile, tempPos2, Quaternion.identity);
				boardTileBG2.name = $"Tile - {i + total - xBlackSize + 1} , {j}";

				Vector2 tempPos3 = new Vector2(i + total - xBlackSize + 1, total - j);
                GameObject boardTileBG3 = Instantiate(blackBoardTile, tempPos3, Quaternion.identity);
				boardTileBG3.name = $"Tile - {i + total - xBlackSize + 1} , {total - j}";

				blackTile.Add(tempPos);
				blackTile.Add(tempPos1);
				blackTile.Add(tempPos2);
				blackTile.Add(tempPos3);

				boardTileBG.transform.SetParent(blackBoardTileContain.transform);
				boardTileBG1.transform.SetParent(blackBoardTileContain.transform);
				boardTileBG2.transform.SetParent(blackBoardTileContain.transform);
				boardTileBG3.transform.SetParent(blackBoardTileContain.transform);
			}
        }

		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				Vector2 tempPos = new Vector2(x, y);

				GameObject newTile = Instantiate(objectTile, tempPos, objectTile.transform.rotation);
				objectTiles[x, y] = newTile;
				newTile.transform.parent = transform;
				newTile.name = $"Tile - {x} , {y}";

				List<Sprite> possibleCharacters = new List<Sprite>();
				possibleCharacters.AddRange(characters);

				possibleCharacters.Remove(previousLeft[y]);
				possibleCharacters.Remove(previousBelow);

				Sprite newSprite = possibleCharacters[Random.Range(0, possibleCharacters.Count)];
				newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
				previousLeft[y] = newSprite;
				previousBelow = newSprite;
				GameObject boardTileBG = Instantiate(boardTile, tempPos, Quaternion.identity);
				boardTileBG.transform.SetParent(boardTileContain.transform);
			}
		}
	}

	public IEnumerator FindNullTiles() 
	{
		for (int x = 0; x < xSize; x++) 
		{
			for (int y = 0; y < ySize; y++) 
			{
				if (objectTiles[x, y].GetComponent<SpriteRenderer>().sprite == null) 
				{
					yield return StartCoroutine(ShiftTilesDown(x, y));
					break;
				}
			}
		}

		for (int x = 0; x < xSize; x++) 
		{
			for (int y = 0; y < ySize; y++) 
			{
				objectTiles[x, y].GetComponent<Tile>().ClearAllMatches();
			}
		}
	}

	private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = .06f) {
		IsShifting = true;
		List<SpriteRenderer> renders = new List<SpriteRenderer>();
		int nullCount = 0;

		for (int y = yStart; y < ySize; y++) 
		{
			SpriteRenderer render = objectTiles[x, y].GetComponent<SpriteRenderer>();
			if (render.sprite == null) 
			{
				nullCount++;
			}
			renders.Add(render);
		}

		for (int i = 0; i < nullCount; i++) 
		{
			GUIManager.instance.Score += 50; // Add this line here
			yield return new WaitForSeconds(shiftDelay);
			for (int k = 0; k < renders.Count - 1; k++) 
			{
				renders[k].sprite = renders[k + 1].sprite;
				renders[k + 1].sprite = GetNewSprite(x, ySize - 1);
			}
		}
		IsShifting = false;
	}

	private Sprite GetNewSprite(int x, int y) 
	{
		List<Sprite> possibleCharacters = new List<Sprite>();
		possibleCharacters.AddRange(characters);

		if (x > 0) {
			possibleCharacters.Remove(objectTiles[x - 1, y].GetComponent<SpriteRenderer>().sprite);
		}
		if (x < xSize - 1) {
			possibleCharacters.Remove(objectTiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
		}
		if (y > 0) {
			possibleCharacters.Remove(objectTiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);
		}

		return possibleCharacters[Random.Range(0, possibleCharacters.Count)];
	}

}
