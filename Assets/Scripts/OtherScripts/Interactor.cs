using UnityEngine;

public class Interactor : MonoBehaviour
{
    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsCurrentlyIntercating)
            return;

        if (CutscenePlayer.IsCutsceneCurrentlyPlaying)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            //Debug.DrawRay(ray.origin, ray.direction, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.transform.root.gameObject.TryGetComponent(out IInteractable movableObject))
                {
                    movableObject.Interact();
                }
            }
        }
    }
}
