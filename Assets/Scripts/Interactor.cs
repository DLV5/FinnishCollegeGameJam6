using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Vector3 _screenPosition;

    private void Update()
    {
        if (GameManager.Instance.IsCurrentlyIntercating)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            _screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(_screenPosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<IInteractable>(out IInteractable movableObject))
                {
                    Debug.Log("hit intercatable");
                    movableObject.Interact();
                }
            }
        }
    }

}
