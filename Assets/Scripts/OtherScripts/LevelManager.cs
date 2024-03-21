using System;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static event Action OnPlayerDied;

    [SerializeField] private EmployeeGenerator _employeeGenerator;
    [SerializeField] private ScheduleGenerator _scheduleGenerator;
    [SerializeField] private PeopleManager _peopleManager;

    [SerializeField] private LevelData _levelData;

    [SerializeField] private FirstPersonCamera _camera;

    private GameObject _winScreen;
    private GameObject _loseScreen;

    private int _playerHealth;

    public int PlayerHealth {
        get => _playerHealth;
        set
        {
            _playerHealth = value;
            if(_playerHealth <= 0)
            {
                Debug.LogWarning("Show lose screen");
                _loseScreen.SetActive(true);
                UnlockCursorAndFreezeCamera();
                OnPlayerDied?.Invoke();
            }
        }
    }

    private bool _IsProcessingEmployee;

    private Employee _currentEmployee;

    public Employee CurrentEmployee { get => _currentEmployee; private set { _currentEmployee = value;} }

    private void Start()
    {
        _winScreen = GameObject.Find("WinScreen");
        _loseScreen = GameObject.Find("LoseScreen"); 

        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);

        PlayerHealth = _levelData.PlayerHealthPoints;

        InitializeEmployees();
        InitializeGameplayLoop();
    }
    public void ShowWinScreen()
    {
        _winScreen.SetActive(true);
        UnlockCursorAndFreezeCamera();
    }

    private void InitializeEmployees()
    {
        for (int i = 0; i < _levelData.NumberOfPeopleToWin; i++)
        {
            _employeeGenerator.GenerateEmployee();
        }

        _scheduleGenerator.InitializeSchedule(_employeeGenerator.Employees);
    }

    private void InitializeGameplayLoop()
    {
        _peopleManager.StartSpawningEmployees(_employeeGenerator, this);
        Debug.Log(" InitializeGameplayLoop ");
    }

    private void UnlockCursorAndFreezeCamera()
    {
        _camera.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
