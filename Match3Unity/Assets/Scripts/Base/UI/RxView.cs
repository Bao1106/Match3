
using UnityEngine;
using UniRx;
using System;


public class RxView<IssuedValueByIssueType> : MonoBehaviour
{
    protected IObservable<IssuedValueByIssueType> objectObservation;

    protected virtual void Start()
    {
        ObservationTargetDesignation();
        ObservationTargetSubscriptionStart();
    }

    protected void ObservationTargetSubscriptionStart()
    {
        objectObservation
            .Subscribe(ObservingObjectValueIssuance)
            .AddTo(this);
    }

    protected virtual void ObservationTargetDesignation()
    {

    }

    protected virtual void ObservingObjectValueIssuance(IssuedValueByIssueType issueValue)
    {

    }
}
