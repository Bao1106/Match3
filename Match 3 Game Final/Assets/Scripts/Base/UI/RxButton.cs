
using UniRx;
using UnityEngine.UI;


public class RxButton : RxUI<Button, Unit>
{
    protected override void ObservationTargetDesignation()
    {
        objectObservation = Applicable.OnClickAsObservable();
    }
    protected override void ObservingObjectValueIssuance(Unit value)
    {
        OnClickAsync();
    }

    public virtual void OnClickAsync()
    {

    }
}

