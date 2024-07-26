using UnityEngine;
using System;

public class CameraZoom : MonoBehaviour
{
    public event Action<float> OnZoom;

    public float MinCamSize => minCamSize;
    public float MaxCamSize => maxCamSize;
    public float InitialCamSize => initialCamSize;

    [SerializeField] private Camera cam;

    [Header("Settings")]
    [SerializeField] [Min(0f)] private float deltaCamSizePerFrame;
    [SerializeField] [Min(0f)] private float minCamSize = 3f;
    [SerializeField] [Min(0f)] private float maxCamSize = 5f;

    private float initialCamSize;

    private void Awake() => initialCamSize = cam.orthographicSize;

    private void Update()
    {
        float _mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        float _delta = 0f;
        if (_mouseScroll > 0f)
            _delta = -deltaCamSizePerFrame;

        else if (_mouseScroll < 0f)
            _delta = deltaCamSizePerFrame;

        float _newOrthoSize = Mathf.Clamp(cam.orthographicSize + _delta, minCamSize, maxCamSize);
        cam.orthographicSize = _newOrthoSize;

        if (_delta != 0f)
            OnZoom?.Invoke(_newOrthoSize);
    }
}