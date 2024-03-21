using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }
    private void Awake()
    {

        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        if(Instance == null)
        {
            Instance = this;
        }

        State = GameState.Playing;
    }
    public bool IsCurrentlyIntercating;
    public float ZoomSpeed;
    public GameState State;
    
}
public enum GameState
{
    Playing,
    NotPlaying
}
