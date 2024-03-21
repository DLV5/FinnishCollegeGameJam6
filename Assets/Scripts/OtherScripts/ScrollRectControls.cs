using UnityEngine;

public class ScrollRectControls : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;

    private float _senscetivity = 200f;

    void Update()
    {
        float input = Input.GetAxis("Vertical");

        if (input != 0)
        {
            _rectTransform.localPosition += new Vector3(0, -input, 0) * Time.deltaTime * _senscetivity;
        }
    }
}
