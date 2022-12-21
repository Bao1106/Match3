using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGameButton : RxButton
{
    public override void OnClickAsync()
    {
        SceneManager.LoadScene("GameScene");
    }
}

