using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _shouldFireWorker;

    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        AudioManager.Instance.PlaySFX("ButtonClick");
        _levelManager.DecideFateOfTheWorker(_shouldFireWorker);
    }
}
