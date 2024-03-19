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
        DontDestroyOnLoad(this);
    }
    public bool IsCurrentlyIntercating;
    public float ZoomSpeed;
}
