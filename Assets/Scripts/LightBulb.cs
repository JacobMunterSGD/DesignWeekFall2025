using UnityEngine;

public class LightBulb : MonoBehaviour
{
    public bool isOn;
    [SerializeField] float timeUntilOff;

    SpriteRenderer spriteRenderer;

    [Header("Input")]
    [SerializeField] KeyCode keyCode;
    [SerializeField] bool isClickInput;

    void Start()
    {
        isOn = true;
        timeUntilOff = GetNewCountdown();
        spriteRenderer = GetComponent<SpriteRenderer>();

        ToggleLight(isOn);
    }

    void Update()
    {
        if (!GameManager.Instance.gameOver) LightsActive();
    }

    void LightsActive()
    {
        // timer
        if (timeUntilOff > 0) timeUntilOff -= Time.deltaTime;
        else
        {
            isOn = false;
            ToggleLight(isOn);
        }

        // player input
        if (Input.GetKeyDown(keyCode) || Input.GetKeyUp(keyCode))
        {
            isOn = !isOn;
            if (isOn) timeUntilOff = GetNewCountdown();
            ToggleLight(isOn);
        }

        if (isClickInput) ClickVariant();
    }

    float GetNewCountdown()
    {
        return Random.Range(LightManager.Instance.countdownMedian - LightManager.Instance.countdownDeviance,
                            LightManager.Instance.countdownMedian + LightManager.Instance.countdownDeviance);
    }

    public void ToggleLight(bool isTurningOn)
    {
        if (isTurningOn)
        {
            spriteRenderer.color = Color.yellow;
        }
        else
        {
            spriteRenderer.color = Color.black;
            LightManager.Instance.CheckAllLights();
        }

    }

    void ClickVariant()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            isOn = !isOn;
            if (isOn) timeUntilOff = Random.Range(3f, 10f);
            ToggleLight(isOn);
        }
    }


}
