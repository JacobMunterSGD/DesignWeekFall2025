using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

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
    }

    private void Update()
    {
        currentTime = Time.time;

        print((startTime + currentTime) / timeToFastestLights);

        float curvedT = countMedianDecreaseCurve.Evaluate((startTime + currentTime) / timeToFastestLights);
        countdownMedian = Mathf.Lerp(startMedian, endMedian, curvedT);
    }

    public void CheckAllLights()
    {
        bool allLightsOff = true;

        foreach (LightBulb light in lightBulbs)
        {
            if (light.isOn)
            {
                allLightsOff = false;
                break;
            }
        }

        if (allLightsOff)
        {
            // all lights off - trigger end game thing
            print("all lights turned off");
            GameManager.Instance.TriggerEndGameSequence();
        }
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
