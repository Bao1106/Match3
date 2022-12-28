using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class SettingPopupActive : RxActive
{
    protected override void ObservationTargetDesignation()
    {
        objectObservation = BoardManager.instance.openSettingPopup;
    }
}
