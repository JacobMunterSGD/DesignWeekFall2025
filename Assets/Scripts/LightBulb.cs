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
        // player input
        if (Input.GetKeyDown(keyCode) || Input.GetKeyUp(keyCode))
        {
            isOn = !isOn;
            if (isOn) timeUntilOff = GetNewCountdown();
            ToggleLight(isOn);
            return;
        }

        if (isClickInput) ClickVariant();

        // timer
        if (timeUntilOff > 0) timeUntilOff -= Time.deltaTime;
        else
        {
            bool wasJustOn = isOn;
            isOn = false;
            if (wasJustOn) ToggleLight(isOn);
            return;
        }        

    }

    float GetNewCountdown()
    {
        float ranRange = Random.Range(LightManager.Instance.countdownMedian - LightManager.Instance.countdownDeviance,
                            LightManager.Instance.countdownMedian + LightManager.Instance.countdownDeviance);
        //print(ranRange);
        return ranRange;
    }

    public void ToggleLight(bool isTurningOn)
    {
        if (isTurningOn)
        {
            spriteRenderer.color = Color.yellow;
            GetNewCountdown();
        }
        else
        {
            spriteRenderer.color = Color.black;
            LightManager.Instance.CheckAllLights();
            timeUntilOff = 0;
        }

        print($"toggled button {gameObject.name}");

    }

    void ClickVariant()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            isOn = !isOn;
            if (isOn) timeUntilOff = Random.Range(3f, 10f);
            ToggleLight(isOn);
        }
        print($"toggled button {gameObject.name}");
    }


}
