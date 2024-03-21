using UnityEngine;

public class ButtonInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private bool _shouldFireWorker;
    [SerializeField] private Animator _animator;

    private LevelManager _levelManager;
    private CutscenePlayer _cutscenePlayer;

    private void Awake()
    {
        _levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        _cutscenePlayer = GameObject.Find("CutscenePlayer").GetComponent<CutscenePlayer>();
    }

    public void Interact()
    {
        Debug.Log("Interacting");
        _animator.SetTrigger("OnButtonWasPressed");
        AudioManager.Instance.PlaySFX("ButtonClick");
        _cutscenePlayer.PlayCutscene();
        _levelManager.DecideFateOfTheWorker(_shouldFireWorker);
    }
}
