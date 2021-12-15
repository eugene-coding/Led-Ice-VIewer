using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// It`s a temporary solution for only one figure at the scene
public class DayPart : MonoBehaviour
{
    [SerializeField] private GameObject _dayPrefab;
    [SerializeField] private GameObject _nightPrefab;

    [SerializeField] private Material _skyboxDay;
    [SerializeField] private Material _skyboxNight;

    [SerializeField] private Volume _volume;

    private bool _isDay;

    private void Start()
    {
        SetToDay();
    }

    public void Change()
    {
        if (_isDay)
        {
            SetToNight();
        }
        else
        {
            SetToDay();
        }

        DynamicGI.UpdateEnvironment();
    }

    private void SetToDay()
    {
        _dayPrefab.SetActive(true);
        _nightPrefab.SetActive(false);

        RenderSettings.skybox = _skyboxDay;
        RenderSettings.sun.enabled = true;

        _volume.enabled = false;

        _isDay = true;
    }

    private void SetToNight()
    {
        _dayPrefab.SetActive(false);
        _nightPrefab.SetActive(true);

        RenderSettings.skybox = _skyboxNight;
        RenderSettings.sun.enabled = false;

        _volume.enabled = true;

        _isDay = false;
    }
}
