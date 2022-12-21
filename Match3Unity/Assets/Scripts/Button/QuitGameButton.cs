using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGameButton : RxButton
{
    public override void OnClickAsync()
    {
        Application.Quit();
    }
}

