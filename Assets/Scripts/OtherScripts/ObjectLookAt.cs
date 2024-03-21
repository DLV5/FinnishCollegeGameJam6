using UnityEngine;

public class ObjectLookAt : MonoBehaviour
{
    [SerializeField] private bool _shouldBeFlipped = false;

    private float direction;

    private void Start()
    {
        direction = _shouldBeFlipped ? -1 : 1;
    }

    private void LateUpdate()
    {
        transform.LookAt(
            transform.position + Camera.main.transform.rotation * Vector3.forward * direction, 
            Camera.main.transform.rotation * Vector3.up
            );
    }
}
