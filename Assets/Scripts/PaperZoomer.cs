using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperZoomer : MonoBehaviour
{
    private Vector3 _screenPosition;
    private Vector3 _worldPosition;
    private Transform _observePoint;
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(_screenPosition);
        if(Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.gameObject.CompareTag("Interactable"))
            {
                StartCoroutine(ZoomToCamera(hit.transform));         
            }
        }
        IEnumerator ZoomToCamera(Transform movableObject)
        {
            float timeElapsed = 0;
            Transform objectInitTransform = movableObject;
            while(true)
            {
                movableObject.transform.position = Mathf.SmoothDamp(movableObject.transform, );
            }
        }
    }
}
