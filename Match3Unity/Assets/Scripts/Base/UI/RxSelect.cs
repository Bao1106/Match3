using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class RxSelect : MonoBehaviour
{

    public GameObject selectHighlightObject;

    public ReactiveProperty<bool> isSelect = new ReactiveProperty<bool>(false);

    protected virtual void Awake()
    {
        isSelect.Subscribe(_ => subscribeIsSelect(_)).AddTo(this);
    }

    protected virtual void subscribeIsSelect(bool isSelect)
    {

    }

}
