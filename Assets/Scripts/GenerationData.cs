using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenerationData", menuName = "ScriptableObjects/GenerationData", order = 1)]
public class GenerationData : ScriptableObject
{
    [Header("Names")]
    public List<string> MaleNames;
    public List<string> FemaleNames;

    [Header("Sprites")]
    public List<Sprite> MaleSprites;
    public List<Sprite> FemaleSprites;

    [Header("Other")]
    public List<string> Excuses;
    public float ProbabilityOfBeingLate;
}
