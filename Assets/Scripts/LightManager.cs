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
        startTime = Time.time;
        //StartCoroutine(LateLightCheck());
    }

    //IEnumerator LateLightCheck()
    //{
    //    yield return new WaitForSeconds(.1f);

    //    LitLightCount();
    //}

    private void Update()
    {
        if (!GameManager.Instance.tutorialOver) TutorialLightCheck();

        else if (GameManager.Instance.gameOver || !GameManager.Instance.tutorialOver) return;
        UpdateMedianTime();
    }

    void TutorialLightCheck()
    {
        if (LitLightCount() == lightBulbs.Count) GameManager.Instance.EndTutorial();
    }

    void UpdateMedianTime()
    {
        currentTime = Time.time;

        float curvedT = countMedianDecreaseCurve.Evaluate((startTime + currentTime) / timeToFastestLights);
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
