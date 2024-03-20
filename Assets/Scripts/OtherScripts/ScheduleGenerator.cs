using System.Collections.Generic;
using UnityEngine;

public class ScheduleGenerator : MonoBehaviour
{
    [SerializeField] private Transform _infoParent;
    [SerializeField] private InfoObject _infoPrefab;
    public void InitializeSchedule(List<Employee> employees)
    {
        while (employees.Count > 0)
        {
            InfoObject instance = Instantiate(_infoPrefab, _infoParent);
            int randomIndex = Random.Range(0, employees.Count);
            instance.Initialize(employees[randomIndex]);
            employees.RemoveAt(randomIndex);
        }
    }
}