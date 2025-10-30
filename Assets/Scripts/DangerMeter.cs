using UnityEngine;
using UnityEngine.UI;

public class DangerMeter : MonoBehaviour
{

    public static DangerMeter Instance;

    public Slider dangerSlider;

    [Header("danger meter parameters")]
    [SerializeField] float maxValue;
    [SerializeField] float startValue;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        dangerSlider.maxValue = maxValue;
        dangerSlider.value = startValue;
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver) return;

        CheckIfLost();
        UpdateSliderGradualIdea();
    }

    void CheckIfLost()
    {
        if (dangerSlider.value == dangerSlider.minValue)
        {
            //GameManager.Instance.TriggerEndGameSequence();
        }
    }

    void UpdateSliderGradualIdea()
    {
        int _lightsOn = LightManager.Instance.CheckAllLights();

        float sliderMoveValue = 0;

        switch (_lightsOn)
        {
            case 0:
                sliderMoveValue = -2;
                break;
            case 1:
                sliderMoveValue = -2;
                break;
            case 2:
                sliderMoveValue = -1;
                break;
            case 3:
                break;
            case 4:
                sliderMoveValue = 1;
                break;
            case 5:
                sliderMoveValue = 2;
                break;

        }

        UpdateSlider(sliderMoveValue * Time.deltaTime);
    }

    public void UpdateSlider(float changeBy)
    {
        dangerSlider.value += changeBy;
    }

    public void SetSlider(float newValue)
    {
        dangerSlider.value = newValue;
    }

}
