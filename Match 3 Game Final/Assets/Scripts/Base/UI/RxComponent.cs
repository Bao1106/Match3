
using UnityEngine;

public class RxComponent<U, T> : RxView<T> where U : Component
{
    protected U Applicable;

    void Awake()
    {
        Applicable = GetComponent<U>();
    }
}
