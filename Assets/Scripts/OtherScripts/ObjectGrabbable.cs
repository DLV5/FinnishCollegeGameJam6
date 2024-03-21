using System;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class ObjectGrabbable : MonoBehaviour
{
    public static event Action OnGrabbed;
    public static event Action OnReleased;

    private Rigidbody objectRigidbody;
    private ObjectLookAt objectLookAt;
    private Transform objectGrabPointTransform;

    private void Awake()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        objectLookAt = GetComponent<ObjectLookAt>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        objectRigidbody.useGravity = false;

        objectLookAt.enabled = true;

        OnGrabbed?.Invoke();
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        objectRigidbody.useGravity = true;

        objectLookAt.enabled = false;

        OnReleased?.Invoke();
    }

    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.MovePosition(newPosition);
        }
    }
}