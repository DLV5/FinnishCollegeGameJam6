using TMPro;
using UnityEngine;

public class DescriptionText : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;

    private bool _shouldShowDecription = true;

    private void OnEnable()
    {
        ObjectGrabbable.OnGrabbed += OnGrabbed;
        ObjectGrabbable.OnReleased += OnReleased;
    }
    
    private void OnDisable()
    {
        ObjectGrabbable.OnGrabbed -= OnGrabbed;
        ObjectGrabbable.OnReleased -= OnReleased;
    }

    public void ShowDescription(string text)
    {
        if(_shouldShowDecription)
            _descriptionText.text = text;
    }

    public void HideDescription()
    {
        _descriptionText.text = string.Empty;
    }

    private void OnGrabbed()
    {
        _shouldShowDecription = false;
        HideDescription();
    }
    
    private void OnReleased()
    {
        _shouldShowDecription = true;
    }
}
