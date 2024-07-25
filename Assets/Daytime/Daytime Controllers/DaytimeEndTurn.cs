using UnityEngine;
using UnityEngine.UI;

public class DaytimeEndTurn : DaytimeController
{
    [SerializeField] private Button turnEndButton;

    private void Awake() => turnEndButton.onClick.AddListener(InvokeOnDayEnd);
}