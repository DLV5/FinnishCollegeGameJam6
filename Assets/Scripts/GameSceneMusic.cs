using UnityEngine;

public class GameSceneMusic : MonoBehaviour
{
    [SerializeField] private string _musicName;
    void Start()
    {
        AudioManager.Instance.PlayMusic(_musicName);
    }
}
