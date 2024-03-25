using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class EmployeeVisualisation : MonoBehaviour
{
    public event Action OnPlaceArrived;
    public Employee Employee { get; private set; }

    [SerializeField] private Material _materialToShow;
    [SerializeField] private Material _nextMaterialToShow;

    [SerializeField] private MeshRenderer _renderer;

    //Contains spawnPoint -> startPoint -> waitPoint -> endPoint -> destroyPoint
    private Queue<Vector3> _pathToMove = new Queue<Vector3>();

    private Vector3 _spawnPoint;
    private Vector3 _startPoint;
    private Vector3 _waitPoint;
    private Vector3 _endPoint;
    private Vector3 _destroyPoint;

    private float _speed;

    private static Queue<Material> _materials = new Queue<Material>();

    private void Awake()
    {
        if (_materials.Count != 0)
            AddMaterialsToQueue();
    }

    public void SetUp(Employee employee, Queue<Vector3> pathToMove)
    {
        SetUpEmployee(employee);

        AddMaterialsToQueue();
        SetUpMaterials();

        SetUpPath(pathToMove);

        SetUpPoints();

        SetUpSpeed(_speed);

        SetUpPosition(_spawnPoint);
    }

    public void SetUpSpeed(float speed) => _speed = speed;

    public void MoveToNextPoint() => StartCoroutine(MoveToTarget(GetNextPointToMove()));

    public void EndMoving()
    {
        Destroy(gameObject);
        //StartCoroutine(MoveAway());
    }

    private void SetUpEmployee(Employee employee) => Employee = employee;

    private void SetUpMaterials()
    {
        Material mainMaterial = _materials.Dequeue();
        mainMaterial.mainTexture = Employee.Look;
        _renderer.material = mainMaterial;
        _materials.Enqueue(mainMaterial);
    }

    private void SetUpPath(Queue<Vector3> pathToMove) => _pathToMove = pathToMove;

    private void SetUpPoints()
    {
        _spawnPoint = GetNextPointToMove();
        _startPoint = GetNextPointToMove();
        _waitPoint = GetNextPointToMove();
        _endPoint = GetNextPointToMove();
        _destroyPoint = GetNextPointToMove();
    }

    private void SetUpPosition(Vector3 position) => transform.position = position;


    private Vector3 GetNextPointToMove() => _pathToMove.Dequeue();

    private void AddMaterialsToQueue()
    {
        _materials.Enqueue(_materialToShow);
        _materials.Enqueue(_nextMaterialToShow);
    }


    private IEnumerator MoveToTarget(Vector3 target)
    {
        float elapsedTime = 0f;
        float procentageComplete = 0f;

        while (true)
        {
            yield return Time.deltaTime;

            elapsedTime += Time.deltaTime;
            procentageComplete = elapsedTime / _speed;

            transform.position = Vector3.Lerp(transform.position, target, procentageComplete);

            if ((transform.position - target).magnitude < 0.1f)
            {
                transform.position = target;
                yield break;
            }
        }
    }
        //while (true)
        //{
        //    yield return Time.deltaTime;
        //    elapsedTime += Time.deltaTime;
        //    procentageComplete = elapsedTime / _speed;
        //    transform.position = Vector3.Lerp(transform.position, _waitPoint, procentageComplete);
        //    if ((transform.position - _waitPoint).magnitude < 0.1f)
        //    {
        //        transform.position = _waitPoint;
        //        OnPlaceArrived?.Invoke();
        //        yield break;
        //    }
        //}

    //IEnumerator MoveAway()
    //{
    //    float elapsedTime = 0f;
    //    float procentageComplete = 0f;
    //    transform.position = _waitPoint;
    //    while (true)
    //    {
    //        yield return Time.deltaTime;
    //        elapsedTime += Time.deltaTime;
    //        procentageComplete = elapsedTime / _speed;
    //        transform.position = Vector3.Lerp(transform.position, _endPoint, procentageComplete);
    //        if ((transform.position - _endPoint).magnitude < 0.1f)
    //        {
    //            transform.position = _endPoint;
    //            break;
    //        }
    //    }
    //    while (true)
    //    {
    //        yield return Time.deltaTime;
    //        elapsedTime += Time.deltaTime;
    //        procentageComplete = elapsedTime / 4f;
    //        transform.position = Vector3.Lerp(transform.position, _destroyPoint, procentageComplete);
    //        if ((transform.position - _destroyPoint).magnitude < 0.1f)
    //        {
    //            break;
    //        }
    //    }
    //    Destroy(gameObject);
    //}

}

