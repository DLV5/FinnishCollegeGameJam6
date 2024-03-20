using System;
using System.Linq;
using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static event Action OnPlayerDied;
    public event Action<Employee> OnNewEmployeeCame;

    [SerializeField] private EmployeeGenerator _employeeGenerator;
    [SerializeField] private ScheduleGenerator _scheduleGenerator;

    [SerializeField] private LevelData _levelData;

    [SerializeField] private FirstPersonCamera _camera;

    [SerializeField] private TMP_Text _resultText;

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

    private Employee _currentEmployee;

    public Employee CurrentEmployee { get => _currentEmployee; private set { _currentEmployee = value;} }

    private void Start()
    {
        _winScreen = GameObject.Find("WinScreen");
        _loseScreen = GameObject.Find("LoseScreen"); 
        _resultText = GameObject.Find("ResultText").GetComponent<TMP_Text>();

        _winScreen.SetActive(false);
        _loseScreen.SetActive(false);

        PlayerHealth = _levelData.PlayerHealthPoints;

        InitializeEmployees();
    }

    private void InitializeEmployees()
    {
        for (int i = 0; i < _levelData.NumberOfPeopleToWin; i++)
        {
            _employeeGenerator.GenerateEmployee();
        }

        _scheduleGenerator.InitializeSchedule(_employeeGenerator.Employees.ToList());

        _currentEmployee = _employeeGenerator.GetNextEmployee();
        OnNewEmployeeCame?.Invoke(_currentEmployee);
        _currentEmployee.DebugShowEmployee();

    }

    public void DecideFateOfTheWorker(bool shouldBeFired)
    {
        if(_currentEmployee.IsLate == shouldBeFired)
        {
            Debug.LogWarning("Correct");
            _resultText.text = "Correct";
        } else
        {
            Debug.LogWarning("Incorrect");
            Debug.LogWarning("-1HP");
            _resultText.text = "Incorrect" + " -1HP";
            PlayerHealth--;
        }
        try
        {
            _currentEmployee = _employeeGenerator.GetNextEmployee();
        }
        catch
        {
            Debug.LogWarning("Show win screen");
            _winScreen.SetActive(true);
            UnlockCursorAndFreezeCamera();
        }

        _currentEmployee.DebugShowEmployee();

        OnNewEmployeeCame?.Invoke(_currentEmployee);
    }

    private void UnlockCursorAndFreezeCamera()
    {
        _camera.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
