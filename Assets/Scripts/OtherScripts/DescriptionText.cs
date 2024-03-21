using TMPro;
using UnityEngine;

public class DescriptionText : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionText;

    public void ShowDescription(string text)
    {
        _descriptionText.text = text;
    }

    public void HideDescription()
    {
        _descriptionText.text = string.Empty;
    }
}
