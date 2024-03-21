using UnityEngine;

public class LookDetection : MonoBehaviour
{
    [SerializeField] private DescriptionText _descriptionText;

    [SerializeField] private float _maxRaycastDistance = 100f;
    [SerializeField] private LayerMask _layerMask;

    private Outline _currentOutline;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, _maxRaycastDistance, _layerMask) || _currentOutline == null)
        {
            if(hit.collider != null && hit.collider.TryGetComponent<Outline>(out Outline outline)) { 
                outline.enabled = true;
                _currentOutline = outline;
                _descriptionText.ShowDescription(
                        hit.
                        collider.
                        GetComponent<DynamicObjectDescription>().
                        ObjectDescription
                        );
            }
        } else if(_currentOutline != null)
        {
            _currentOutline.enabled = false;
            _currentOutline = null;
            _descriptionText.HideDescription();
        }

    }
}
