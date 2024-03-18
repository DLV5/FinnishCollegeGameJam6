using System;
using System.Globalization;
using UnityEngine;

public class Employee
{
    public string Name {  get; private set; }
    public string Surname { get; private set; }

    public int Age {  get; private set; }
    public Gender Gender {  get; private set; }
    public DateTime DateOfBirth {  get; private set; }

    public DateTime ArrivedAtTime {  get; private set; }
    public bool IsLate {  get; private set; }
    public string ReasonForBeingLate { get; private set; }

    public Employee(string name, string surname, DateTime dateOfBirth, DateTime arrivedAtTime, bool isLate, string reasonForBeingLate)
    {
        Name = name;
        Surname = surname;
        DateOfBirth = dateOfBirth;
        Age = DateTime.Now.Year - dateOfBirth.Year;

        ArrivedAtTime = arrivedAtTime;
        ReasonForBeingLate = reasonForBeingLate;
        
        IsLate = isLate;

        DebugShowEmployee();
    }

    public void DebugShowEmployee()
    {
        Debug.Log($"My fullname: {Name} {Surname}");
        Debug.Log($"My date of birth: {DateOfBirth.Date.ToShortDateString().ToString(CultureInfo.InvariantCulture)}");
        Debug.Log($"My age: {Age}");
        Debug.Log($"When I came to work today: {ArrivedAtTime.ToShortTimeString()}");
        Debug.Log($"Am I late: {IsLate}");
        Debug.Log($"My excuse: {ReasonForBeingLate}");
    }
}
