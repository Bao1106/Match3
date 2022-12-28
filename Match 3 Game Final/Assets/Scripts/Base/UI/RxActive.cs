using UnityEngine;
public class RxActive : RxView<bool>
{
    protected override void ObservingObjectValueIssuance(bool value)
    { 
        gameObject.SetActive(value);
    }
}