using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    [Tooltip("Amount of people, that player have to check before the end of the level")]
    public int NumberOfPeopleToWin;

    public int DeadlineTimeHours = 9;
    public int DeadlineTimeMinutes;

    public int PlayerHealthPoints = 3;
}
