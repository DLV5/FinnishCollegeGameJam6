using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GenerationData", menuName = "ScriptableObjects/GenerationData", order = 2)]
public class GenerationData : ScriptableObject
{
    [Header("Names")]
    public List<string> MaleNames;
    public List<string> FemaleNames;

    public List<string> Surnames;

    [Header("Sprites")]
    public List<Texture> MaleTextures;
    public List<Texture> FemaleTextures;

    [Header("Other")]
    [Range(0, 1)]public float ProbabilityOfBeingLate;
    public List<string> Excuses;
}
