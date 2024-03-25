using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PeopleManager : MonoBehaviour
{
    public static event Action<Employee> OnNewEmployeeCame; //used, for example, to generate new documents for each employee
    public static event Action OnEmployeeWentAway;

    [Header("Employee parameters")]
    [SerializeField] private EmployeeVisualisationFactory _employeeObjectFactory;
    [SerializeField] private Transform _employeeParent;
    [SerializeField] private EmployeeVisualisation _employeePrefab;
    [SerializeField] private float _speed;

    [Header("Employee moving points")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _waitPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _destroyPoint;

    private Queue<Vector3> _pointsPathToMove = new Queue<Vector3>();

    private TMP_Text _resultText;

    private LevelManager _levelManager;
    private EmployeeGenerator _employeeGenerator;

    private EmployeeVisualisation _currentEmployee;

    private bool _isCurrentlyProsessing;

    private CutscenePlayer _cutscenePlayer;

    private void Start()
    {
        _cutscenePlayer = GameObject.Find("CutscenePlayer").GetComponent<CutscenePlayer>();
    }

    private void OnEnable()
    {
        CutscenePlayer.OnCutsceneEnded += SpawnNextEmployee;
    }
    
    private void OnDisable()
    {
        CutscenePlayer.OnCutsceneEnded -= SpawnNextEmployee;
    }

    public void SpawnNextEmployee()
    {
        _isCurrentlyProsessing = false;
        Employee employeeToGet = _employeeGenerator.GetNextEmployee();
        _currentEmployee = 
            _employeeObjectFactory.GetEmployee(employeeToGet, _pointsPathToMove);
        _currentEmployee.OnPlaceArrived 
            += delegate { 
                OnNewEmployeeCame?
                .Invoke(_currentEmployee
                        .Employee
                        ); 
                _isCurrentlyProsessing = true; };  // Invoking OnNewEmployeeCame, DocumentGanerator is the subscriber 

        _currentEmployee.SetUpSpeed(_speed);
        _currentEmployee.MoveToNextPoint();
    }

    public void SetUp(EmployeeGenerator employeeGenerator, LevelManager levelManager)
    {
        SetUpResultText();

        SetUpEmployeeGenerator(employeeGenerator);
        SetUpLevelManager(levelManager);

        SetUpEmployeeVisualisationFactory();

        SetUpPathToMove();
        //_currentEmployee.Employee.DebugShowEmployee();
    }

    private void SetUpResultText() =>
        _resultText = GameObject.Find("ResultText").GetComponent<TMP_Text>();

    private void SetUpEmployeeGenerator(EmployeeGenerator employeeGenerator) 
        => _employeeGenerator = employeeGenerator;

    private void SetUpLevelManager(LevelManager levelManager) => _levelManager = levelManager;

    private void SetUpEmployeeVisualisationFactory() 
        => _employeeObjectFactory = new EmployeeVisualisationFactory(_employeeParent, _employeePrefab);

    private void SetUpPathToMove()
    {
        _pointsPathToMove.Enqueue(_spawnPoint.position);
        _pointsPathToMove.Enqueue(_startPoint.position);
        _pointsPathToMove.Enqueue(_waitPoint.position);
        _pointsPathToMove.Enqueue(_endPoint.position);
        _pointsPathToMove.Enqueue(_destroyPoint.position);
    }

    private void RemoveCurrentEmployee()
    {
        _currentEmployee.EndMoving();
        _currentEmployee = null;
    }
    public void DecideFateOfTheWorker(bool shouldBeFired)
    {
        if (!_isCurrentlyProsessing || GameManager.Instance.State != GameState.Playing)
            return;

        if (CutscenePlayer.IsCutsceneCurrentlyPlaying)
            return;

        if (shouldBeFired)
        {
            _cutscenePlayer.PlayCutscene();
        }

        if (_currentEmployee.Employee.IsLate == shouldBeFired)
        {
            _resultText.text = "Correct";
        }
        else
        {
            _resultText.text = "Incorrect" + " -1HP";
            _levelManager.PlayerHealth--;
        }

        RemoveCurrentEmployee();

        try
        {
            if(!CutscenePlayer.IsCutsceneCurrentlyPlaying)
                SpawnNextEmployee();
        }
        catch
        {
            _isCurrentlyProsessing = false;
            _resultText.text = "You Won! ";
            _levelManager.ShowWinScreen();
        }
    }
}
