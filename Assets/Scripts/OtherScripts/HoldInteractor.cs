using UnityEngine;

public class HoldInteractor : MonoBehaviour
{
    [SerializeField] private Transform _objectGrabPointTransform;

    private float _mouseSensetivity = 0.05f;
    private ObjectGrabbable _objectGrabbable;

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsCurrentlyIntercating)
            return;
        
        if (CutscenePlayer.IsCutsceneCurrentlyPlaying)
            return;

        if (_objectGrabbable != null)
        {
            _objectGrabPointTransform.position += Input.mouseScrollDelta.y * Camera.main.transform.forward * _mouseSensetivity;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (_objectGrabbable == null) {
                Ray ray = new Ray(transform.position, transform.forward);
                //Debug.DrawRay(ray.origin, ray.direction, Color.red);

                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    if (hit.collider.gameObject.transform.root.gameObject.TryGetComponent(out ObjectGrabbable toGrab))
                    {
                            toGrab.Grab(_objectGrabPointTransform);
                            _objectGrabbable = toGrab;
                    }
                } 
            } else
            {
                // Currently carrying something, drop
                _objectGrabbable.Drop();
                _objectGrabbable = null;
            }
        }
    }
}
