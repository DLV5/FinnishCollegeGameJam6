using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static event Action OnPlayerDied;
    public static event Action OnNewEmployeeCame;

    [SerializeField] private EmployeeGenerator _employeeGenerator;
    [SerializeField] private DocumentsGenerator _documentsGenerator;

    [SerializeField] private LevelData _levelData;

    private int _playerHealth;
    public int PlayerHealth {
        get => _playerHealth;
        set
        {
            _playerHealth = value;
            if(_playerHealth <= 0)
            {
                Debug.LogWarning("Show lose screen");
                OnPlayerDied?.Invoke();
            }
        }
    }

    private Employee _currentEmployee;

    private void Start()
    {
        PlayerHealth = _levelData.PlayerHealthPoints;

        InitializeEmployees();
    }

    private void InitializeEmployees()
    {
        for (int i = 0; i < _levelData.NumberOfPeopleToWin; i++)
        {
            _employeeGenerator.GenerateEmployee();
        }

        _currentEmployee = _employeeGenerator.GetNextEmployee();
        _currentEmployee.DebugShowEmployee();

        _documentsGenerator.InitializeDocument();
    }

    public void DecideFateOfTheWorker(bool shouldBeFired)
    {
        if(_currentEmployee.IsLate == shouldBeFired)
        {
            Debug.LogWarning("Correct");
        } else
        {
            Debug.LogWarning("Incorrect");
            Debug.LogWarning("-1HP");
            PlayerHealth--;
        }
        try
        {
            _currentEmployee = _employeeGenerator.GetNextEmployee();
        }
        catch
        {
            Debug.LogWarning("Show win screen");
        }

        _currentEmployee.DebugShowEmployee();

        OnNewEmployeeCame?.Invoke();
    }
}
