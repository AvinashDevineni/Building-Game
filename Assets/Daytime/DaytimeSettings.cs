using UnityEngine;

public class DaytimeSettings : MonoBehaviour
{
    public static DaytimeSetting GameDaytimeSetting { get; private set; } = DaytimeSetting.END_TURN;

    public void SetTimeSetting(DaytimeSetting _newSetting) => GameDaytimeSetting = _newSetting;

    public enum DaytimeSetting { REAL_TIME, END_TURN }
}