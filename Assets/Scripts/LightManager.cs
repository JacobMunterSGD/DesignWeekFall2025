using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LightManager : MonoBehaviour
{

    public static LightManager Instance;

    public List<LightBulb> lightBulbs;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GetAllSceneLights();
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
