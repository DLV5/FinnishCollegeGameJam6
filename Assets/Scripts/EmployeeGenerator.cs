using System;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class EmployeeGenerator : MonoBehaviour
{
    private DateTime deadline = new DateTime(1, 1, 1, 9, 0, 0);
    private int minutesRange = 30;

    [SerializeField] private GenerationData _data;

    public List<Employee> Employees {  get; private set; } = new List<Employee>();

    public void GenerateEmployee()
    {
        Gender gender = (Gender)UnityEngine.Random.Range(0, 2);

        string name;

        if (gender == Gender.Male)
        {
            name = _data.MaleNames[UnityEngine.Random.Range(0, _data.MaleNames.Count)];
        } else
        {
            name = _data.FemaleNames[UnityEngine.Random.Range(0, _data.FemaleNames.Count)];
        }

        string surname = _data.Surnames[UnityEngine.Random.Range(0, _data.Surnames.Count)];

        DateTime dateOfBirth = GetRandomData();

        DateTime arrivalTime = GetRandomTime(_data.ProbabilityOfBeingLate);

        string excuse = _data.Excuses[UnityEngine.Random.Range(0, _data.Excuses.Count)];

        Employee worker = new Employee(
            name, 
            surname, 
            dateOfBirth, 
            arrivalTime, 
            IsEmployeeLate(arrivalTime), 
            excuse);
        Employees.Add(worker);
    }

    private DateTime GetRandomData()
    {
        int randomDay = UnityEngine.Random.Range(1, 31);
        int randomMounth = UnityEngine.Random.Range(1, 13);
        int randomYear = UnityEngine.Random.Range(50, 99);

        return new DateTime(1900 + randomYear, randomMounth, randomDay);
    }
    
    private DateTime GetRandomTime(float probabilityOfBeingLate)
    {
        DateTime randomTime = deadline;
        randomTime = randomTime.AddMinutes(UnityEngine.Random.Range(0, minutesRange) - (1 - probabilityOfBeingLate) * minutesRange);

        return randomTime;
    }

    private bool IsEmployeeLate(DateTime arrivalTime)
    {
        return arrivalTime.TimeOfDay >= deadline.TimeOfDay;
    }
}
