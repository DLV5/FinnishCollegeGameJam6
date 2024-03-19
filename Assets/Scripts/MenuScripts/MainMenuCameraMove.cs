using UnityEngine;

public class MainMenuCameraMove : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float rotationAmount = 30.0f;

    private float initialYRotation;

    void Start()
    {
        // Store the initial rotation of the camera
        initialYRotation = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        // Calculate the new rotation angle based on sinusoidal function
        float sinValue = Mathf.Sin(Time.time * rotationSpeed);
        float targetRotation = initialYRotation + sinValue * rotationAmount;

        // Apply the rotation to the camera
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotation, transform.rotation.eulerAngles.z);
    }
}
