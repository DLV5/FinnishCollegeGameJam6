using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeObject : MonoBehaviour
{
    public event Action OnPlaceArrived;
    public Employee Employee { get; private set; }

    [SerializeField] private Material _previousLook;
    [SerializeField] private Material _look;

    [SerializeField] private MeshRenderer _renderer;

    private Transform _initialPoint;
    private Transform _startPoint;
    private Transform _waitPoint;
    private Transform _endPoint;
    private Transform _destroyPoint;

    private float _speed;

    private static Queue<Material> _materials = new Queue<Material>();

    private void Awake()
    {
        if (_materials.Count != 0)
            return;
        _materials.Enqueue(_look);
        _materials.Enqueue(_previousLook);
    }

    public void SetUp(Employee employee, Transform initialPoint, Transform startPoint, Transform waitPoint, Transform endPoint, Transform destroyPoint)
    {
        Employee = employee;

        Material mainMaterial = _materials.Dequeue();
        mainMaterial.mainTexture = employee.Look;
        _renderer.material = mainMaterial;
        _materials.Enqueue(mainMaterial);

        _initialPoint = initialPoint;
        _startPoint = startPoint;
        _waitPoint = waitPoint;
        _endPoint = endPoint;
        _destroyPoint = destroyPoint;;
    }
    public void StartMoving(float speed)
    {
        _speed = speed;
        StartCoroutine(MoveToTarget());
    }

    public void EndMoving()
    {
        StartCoroutine(MoveAway());
    }
    IEnumerator MoveToTarget()
    {
        float elapsedTime = 0f;
        float procentageComplete = 0f;
        transform.position = _initialPoint.position;
        while (true)
        {
            yield return Time.deltaTime;
            elapsedTime += Time.deltaTime;
            procentageComplete = elapsedTime / 4f;
            transform.position = Vector3.Lerp(transform.position, _startPoint.position, procentageComplete);
            if((transform.position - _startPoint.position).magnitude < 0.1f)
            {
                transform.position = _startPoint.position;
                break;
            }
        }
        while (true)
        {
            yield return Time.deltaTime;
            elapsedTime += Time.deltaTime;
            procentageComplete = elapsedTime / _speed;
            transform.position = Vector3.Lerp(transform.position, _waitPoint.position, procentageComplete);
            if ((transform.position - _waitPoint.position).magnitude < 0.1f)
            {
                transform.position = _waitPoint.position;
                OnPlaceArrived?.Invoke();
                yield break;
            }
        }
    }

    IEnumerator MoveAway()
    {
        float elapsedTime = 0f;
        float procentageComplete = 0f;
        transform.position = _waitPoint.position;
        while (true)
        {
            yield return Time.deltaTime;
            elapsedTime += Time.deltaTime;
            procentageComplete = elapsedTime / _speed;
            transform.position = Vector3.Lerp(transform.position, _endPoint.position, procentageComplete);
            if ((transform.position - _endPoint.position).magnitude < 0.1f)
            {
                transform.position = _endPoint.position;
                break;
            }
        }
        while (true)
        {
            yield return Time.deltaTime;
            elapsedTime += Time.deltaTime;
            procentageComplete = elapsedTime / 4f;
            transform.position = Vector3.Lerp(transform.position, _destroyPoint.position, procentageComplete);
            if ((transform.position - _destroyPoint.position).magnitude < 0.1f)
            {
                break;
            }
        }
        Destroy(gameObject);
    }

}
