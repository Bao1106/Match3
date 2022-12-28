//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WalletAddressActive : RxTextMeshPro<string>
//{
//    protected override void ObservationTargetDesignation()
//    {
//        objectObservation = WalletAddress.myWalletAddress;
//    }

//    protected override string DisplayFormatSpecified(string observationTargetIssueValue)
//    {
//        string value = string.Empty;
//        if (!string.IsNullOrEmpty(observationTargetIssueValue))
//            value = observationTargetIssueValue.Substring(0, 9);
//        return value;
//    }
//}
