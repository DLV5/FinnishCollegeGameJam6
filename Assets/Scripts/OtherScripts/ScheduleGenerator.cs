using System.Collections.Generic;
using UnityEngine;

public class ScheduleGenerator : MonoBehaviour
{
    [SerializeField] private Transform _infoParent;
    [SerializeField] private InfoObject _infoPrefab;
    public void InitializeSchedule(Queue<Employee> employees)
    {
        List<Employee> list = new List<Employee>();
        list.AddRange(employees);
        while (list.Count > 0)
        {
            InfoObject instance = Instantiate(_infoPrefab, _infoParent);
            int randomIndex = Random.Range(0, list.Count);
            instance.Initialize(list[randomIndex]);
            list.RemoveAt(randomIndex);
        }
    }
}