using UnityEngine;
using System;

public abstract class DaytimeController : MonoBehaviour
{
    public event Action OnDayEnd;

    protected void InvokeOnDayEnd() => OnDayEnd?.Invoke();
}