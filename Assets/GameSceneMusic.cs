using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneMusic : MonoBehaviour
{
    [SerializeField] private string _musicName;
    void Start()
    {
        AudioManager.Instance.PlayMusic(_musicName);
    }
}
