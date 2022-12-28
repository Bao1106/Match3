using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : RxButton
{
    public override void OnClickAsync()
    {
        Application.Quit();
    }
}

