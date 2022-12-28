using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnouncementManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI notiText;
    [SerializeField] RectTransform textTransform;
    private float scrollSpeed = 10.0f;
    float startPos = -1500f;
    float endPos = -1450f;
    // Start is called before the first frame update
    void Start()
    {
        textTransform.localPosition = Vector2.left * startPos;
        StartCoroutine(DisplayNotification());
    }

    IEnumerator DisplayNotification()
    {
        

        while (transform.localPosition.x > endPos)
        {
            transform.Translate(Vector2.left * scrollSpeed * Time.deltaTime);
            if (transform.localPosition.x <= endPos)
            {
                textTransform.localPosition = Vector2.left * startPos;
            }

            yield return null;
        }
    }
}
