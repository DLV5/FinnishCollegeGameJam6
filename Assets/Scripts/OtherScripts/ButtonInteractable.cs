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
        PlayButtonAnimation();
        PlayOnPressedSound();
        _peopleManager.DecideFateOfTheWorker(_shouldFireWorker);
    }

    private void PlayButtonAnimation() => _animator.SetTrigger("OnButtonWasPressed");
    private void PlayOnPressedSound() => AudioManager.Instance.PlaySFX("ButtonClick");
}
