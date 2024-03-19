using UnityEngine;

public class LookDetection : MonoBehaviour
{
    public float maxRaycastDistance = 100f;

    private Outline _currentOutline;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit) || _currentOutline == null)
        {
            if(hit.collider != null && hit.collider.TryGetComponent<Outline>(out Outline outline)) { 
                outline.enabled = true;
                _currentOutline = outline;
            }
        } else if(_currentOutline != null)
        {
            _currentOutline.enabled = false;
            _currentOutline = null;
        }
    }
}
