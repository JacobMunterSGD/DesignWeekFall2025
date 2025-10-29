using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class LightManager : MonoBehaviour
{

    public static LightManager Instance;

    public List<LightBulb> lightBulbs;

    [Header("Countdown Variables")]

    public float countdownMedian;
    public float countdownDeviance;

    [SerializeField] AnimationCurve countMedianDecreaseCurve;
    [SerializeField] float timeToFastestLights;
    float startTime;
    float currentTime;

    [SerializeField] float startMedian;
    [SerializeField] float endMedian;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetAllSceneLights();
        countdownMedian = startMedian;
        startTime = Time.time;
        StartCoroutine(LateLightCheck());
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver) return;
        UpdateMedianTime();
    }

    void UpdateMedianTime()
    {
        currentTime = Time.time;

        float curvedT = countMedianDecreaseCurve.Evaluate((startTime + currentTime) / timeToFastestLights);
        countdownMedian = Mathf.Lerp(startMedian, endMedian, curvedT);
    }

    IEnumerator LateLightCheck()
    {
        yield return new WaitForSeconds(.1f);

        CheckAllLights();
    }

    public int CheckAllLights()
    {
        bool allLightsOff = true;
        int totalLightsOn = 0;

        foreach (LightBulb light in lightBulbs)
        {
            if (light.isOn)
            {
                totalLightsOn++;
                allLightsOff = false;
            }
        }

        if (allLightsOff)
        {
            // all lights off - trigger end game thing
            //print("all lights turned off");
            //GameManager.Instance.TriggerEndGameSequence();
        }

        //lightsOnSlider.value = totalLightsOn;

        return totalLightsOn;

    }

    void GetAllSceneLights()
    {
        foreach (Transform childTransform in transform)
        {
            if (childTransform.gameObject.TryGetComponent<LightBulb>(out LightBulb _light))
            {
                lightBulbs.Add(_light);
            }
        }
    }


}
