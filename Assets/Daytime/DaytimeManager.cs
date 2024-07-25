using UnityEngine;
using System;
using System.Collections.Generic;

public class DaytimeManager : MonoBehaviour
{
    public event Action OnDayEnd;

    [SerializeField] private List<TimeSettingLogic> settingLogics;

    private DaytimeController daytimeController = null;

    private void Awake()
    {
        foreach (TimeSettingLogic _logic in settingLogics)
        {
            if (_logic.TimeSetting == DaytimeSettings.GameDaytimeSetting)
            {
                daytimeController = _logic.DaytimeController;
                break;
            }
        }

        if (daytimeController == null)
            Debug.LogError($"No DaytimeController found for TimeSetting of {DaytimeSettings.GameDaytimeSetting}");

        daytimeController.OnDayEnd += () => OnDayEnd?.Invoke();
    }

    [Serializable]
    public class TimeSettingLogic
    {
        [field: SerializeField] public DaytimeSettings.DaytimeSetting TimeSetting { get; private set; }
        [field: SerializeField] public DaytimeController DaytimeController { get; private set; }
    }
}