using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RxTextMeshPro<T> : RxUI<TextMeshProUGUI, T>
{
    protected override void ObservingObjectValueIssuance(T ObservationTargetIssueValue)
    {
        Applicable.text = DisplayFormatSpecified(ObservationTargetIssueValue);
    }
    protected virtual string DisplayFormatSpecified(T ObservationTargetIssueValue)
    {
        return ObservationTargetIssueValue.ToString();
    }
}
