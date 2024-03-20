using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmployeeObject : MonoBehaviour
{
    public event Action OnPlaceArrived;
    public Employee Employee { get; private set; }

    [SerializeField] private Image _look;
    private Transform _initialPoint;
    private Transform _startPoint;
    private Transform _waitPoint;
    private Transform _endPoint;
    private Transform _destroyPoint;

    private float _speed;

    public void SetUp(Employee employee, Transform initialPoint, Transform startPoint, Transform waitPoint, Transform endPoint, Transform destroyPoint)
    {
        Employee = employee;
        _look.sprite = employee.Look;

        _initialPoint = initialPoint;
        _startPoint = startPoint;
        _waitPoint = waitPoint;
        _endPoint = endPoint;
        _destroyPoint = destroyPoint;;
    }
    public void StartMoving()
    {
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
            procentageComplete = elapsedTime / _speed;
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
            procentageComplete = elapsedTime / _speed;
            transform.position = Vector3.Lerp(transform.position, _destroyPoint.position, procentageComplete);
            if ((transform.position - _destroyPoint.position).magnitude < 0.1f)
            {
                Destroy(gameObject);
                yield break;
            }
        }
    }

}
