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

    float startTime;
    float currentTime;

    [SerializeField] float startMedian;
    [SerializeField] float endMedian;

    public float countdownFlickerTime;

    [Header("Sprite References")]
    public Sprite lightOn;
    public Sprite lightOff;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetAllSceneLights();
        countdownMedian = startMedian;
    }

    private void Update()
    {
        if (!GameManager.Instance.tutorialOver) TutorialLightCheck();

        if (!GameManager.Instance.gameOver && GameManager.Instance.tutorialOver) UpdateMedianTime();        
    }

    void TutorialLightCheck()
    {
        if (LitLightCount() == lightBulbs.Count) GameManager.Instance.EndTutorial();
    }

    void UpdateMedianTime()
    {
        if (startTime == 0) return;

        currentTime = Time.time;

        float curvedT = countMedianDecreaseCurve.Evaluate((currentTime - startTime) / WinClock.Instance.timeToWin);

        countdownMedian = Mathf.Lerp(startMedian, endMedian, curvedT);
    }

    public int LitLightCount()
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
        }

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

    public void FlickerAllLights()
    {
        foreach (LightBulb lightBulb in lightBulbs)
        {
            lightBulb.Flicker();
        }
    }

    public void SetMedianStartTime(float value)
    {
        startTime = value;
    }


}
