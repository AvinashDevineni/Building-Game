using UnityEngine;

public class CameraPan : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private Transform camTrans;

    private Vector2 mouseOrigin;

    private void Awake() => camTrans = cam.transform;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            mouseOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector3 _movement = mouseOrigin - (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            camTrans.position += _movement;
        }
    }
}