using UnityEngine;

public class Interactor : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance.IsCurrentlyIntercating)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.transform.root.gameObject.TryGetComponent(out IInteractable movableObject))
                {
                    Debug.Log("hit intercatable");
                    movableObject.Interact();
                }
            }
        }
    }
}
