using UnityEngine;

[RequireComponent (typeof(DynamicObjectDescription))]
public class ButtonInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _shouldFireWorker;
    [SerializeField] private Animator _animator;

    private PeopleManager _peopleManager;

    private void Awake()
    {
        _peopleManager = GameObject.Find("PeopleManager").GetComponent<PeopleManager>();
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        _animator.SetTrigger("OnButtonWasPressed");
        AudioManager.Instance.PlaySFX("ButtonClick");
        _peopleManager.DecideFateOfTheWorker(_shouldFireWorker);
    }
}
