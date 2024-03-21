using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _shouldFireWorker;

    private PeopleManager _peopleManager;

    private void Awake()
    {
        _peopleManager = GameObject.Find("PeopleManager").GetComponent<PeopleManager>();
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        AudioManager.Instance.PlaySFX("ButtonClick");
        _peopleManager.DecideFateOfTheWorker(_shouldFireWorker);
    }
}
