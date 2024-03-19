using System.Collections;
using UnityEngine;

public class IDCard : MonoBehaviour, IInteractable
{
    private Vector3 _initialPosition;
    private Quaternion _initialRotation;

    [SerializeField] private Vector3 _zoomRotation;

    [SerializeField] private Camera _cameraObservePoint;
    [SerializeField] private Vector3 _cameraOffset;

    private bool _waitForInput = false;

    private void Start()
    {
        _initialPosition = transform.position;
        _initialRotation = transform.rotation;
    }
    private void Update()
    {
        if (!_waitForInput)
            return;

        Debug.Log("Waiting for the input");
       if (Input.GetMouseButtonDown(0))
       {
            Debug.Log("Input Was Made!");
            StartCoroutine(UnzoomFromCamera());
            _waitForInput = false;
       }
  
    }
    public void Interact()
    {
        GameManager.Instance.IsCurrentlyIntercating = true;
        StartCoroutine(ZoomToCamera());
    }
    IEnumerator ZoomToCamera()
    {
        float percentageComplete = 0f;
        float elapsedTime = 0f;
        while (true)
        {
            elapsedTime += Time.deltaTime;
            percentageComplete += elapsedTime / GameManager.Instance.ZoomSpeed;

            yield return new WaitForSeconds(Time.deltaTime);

            transform.position = Vector3.Lerp(
                transform.position, 
                _cameraObservePoint.transform.position + _cameraOffset, 
                Mathf.SmoothStep(0, 1, percentageComplete)
                );  //transform position
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, _zoomRotation, Mathf.SmoothStep(0, 1, percentageComplete)));   //transform rotation

            //Debug.Log($"1: {movableObject.transform.position}, and elapsed time: {elapsedTime} and persentage complete: {percentageComplete}");

            if ((transform.position - _cameraObservePoint.transform.position - _cameraOffset).magnitude < 0.001f)
            {
                transform.position = _cameraObservePoint.transform.position + _cameraOffset;
                transform.rotation.SetEulerAngles(_zoomRotation);

                _waitForInput = true;   // Bool to wait for the input for unzooming

                break;
            }
        }
    }
    IEnumerator UnzoomFromCamera()
    {
        float percentageComplete = 0f;
        float elapsedTime = 0f;

        while (true) {
            Debug.Log("Unzooming");
            elapsedTime += Time.deltaTime;
            percentageComplete += elapsedTime / GameManager.Instance.ZoomSpeed;

            yield return new WaitForSeconds(Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, _initialPosition, Mathf.SmoothStep(0, 1, percentageComplete));  //transform position
            transform.rotation = Quaternion.Euler(Vector3.Lerp(transform.rotation.eulerAngles, _initialRotation.eulerAngles, Mathf.SmoothStep(0, 1, percentageComplete)));   //transform rotation

            //Debug.Log($"1: {movableObject.transform.position}, and elapsed time: {elapsedTime} and persentage complete: {percentageComplete}");

            if ((transform.position - _initialPosition).magnitude < 0.001f)
            {
                Debug.Log($"and {transform.position} and {_initialPosition}");
                transform.position = _initialPosition;
                transform.rotation = _initialRotation;

                GameManager.Instance.IsCurrentlyIntercating = false;
                yield break;
            }
        }
    }
}
