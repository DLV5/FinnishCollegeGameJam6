using System;
using System.Collections;
using UnityEngine;

public class CutscenePlayer : MonoBehaviour
{
    public static event Action OnCutsceneStarted;
    public static event Action OnCutsceneEnded;

    public static bool IsCutsceneCurrentlyPlaying = false;

    [SerializeField] private FogAdjuster _fogAdjuster;
    [SerializeField] private GameObject[] _lights = new GameObject[] { };

    public void PlayCutscene()
    {
        AudioManager.Instance.StopPlayMusic();
        StartCoroutine(CutsceneRoutine());
    }

    private IEnumerator CutsceneRoutine()
    {
        if (IsCutsceneCurrentlyPlaying)
            yield break;

        OnCutsceneStarted?.Invoke();

        IsCutsceneCurrentlyPlaying = true;

        StartCoroutine(TurnLightsOff(1f));

        _fogAdjuster.FogFadeIn();

        yield return new WaitForSeconds(2f);

        AudioManager.Instance.PlaySFX("FireEmployee");

        yield return new WaitForSeconds(13f);

        StartCoroutine(TurnLightsOn(.5f));

        yield return new WaitForSeconds(1f);

        _fogAdjuster.FogFadeOut();

        yield return new WaitForSeconds(1f);

        AudioManager.Instance.ResumeMusic();
        AudioManager.Instance.PlayMusic("BackgroundMusic");

        IsCutsceneCurrentlyPlaying = false;

        OnCutsceneEnded?.Invoke();
    }

    private IEnumerator TurnLightsOff(float delay)
    {
        foreach (var light in _lights)
        {
            TurnLightOff(light);
            yield return new WaitForSeconds(delay);
        }
    }
    
    private IEnumerator TurnLightsOn(float delay)
    {
        foreach (var light in _lights)
        {
            TurnLightOn(light);
            yield return new WaitForSeconds(delay);
        }
    }

    private void TurnLightOff(GameObject _light)
    {
        AudioManager.Instance.PlaySFX("TurnLightOff");
        _light.gameObject.SetActive(false);
    }
    private void TurnLightOn(GameObject _light)
    {
        AudioManager.Instance.PlaySFX("TurnLightOn");
        _light.gameObject.SetActive(true);
    }
}
