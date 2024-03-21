using System.Collections;
using UnityEngine;

public class FogAdjuster : MonoBehaviour
{
    private float _normalFogLevel = 0.35f;
    private float _darkFogLevel = 1f;

    private float _speed = 1f;

    public void FogFadeIn()
    {
        StartCoroutine(FogFadeInCoroutine());
    } 
    
    public void FogFadeOut()
    {
        StartCoroutine(FogFadeOutCoroutine());
    }

    private IEnumerator FogFadeInCoroutine()
    {
        while (RenderSettings.fogDensity < _darkFogLevel)
        {
                RenderSettings.fogDensity += _speed * Time.deltaTime;
                yield return Time.deltaTime;
        };
    }
    
    private IEnumerator FogFadeOutCoroutine()
    {
        while (RenderSettings.fogDensity > _normalFogLevel)
        {
                RenderSettings.fogDensity -= _speed * Time.deltaTime;
                yield return Time.deltaTime;
        };
    }
}
