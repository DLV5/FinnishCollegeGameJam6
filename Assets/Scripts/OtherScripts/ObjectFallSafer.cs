using UnityEngine;

public class ObjectFallSafer : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void Update()
    {
        if (transform.position.y < -10)
        {
            transform.position = respawnPoint.position;
        }
    }
}