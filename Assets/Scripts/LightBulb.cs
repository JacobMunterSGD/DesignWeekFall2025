using UnityEngine;

public class LightBulb : MonoBehaviour
{
    public bool isOn;
    [SerializeField] float timeUntilOff;

    SpriteRenderer spriteRenderer;

    [Header("KeyToBePressed")]
    [SerializeField] KeyCode keyCode;
    [SerializeField] bool isClickInput;

    void Start()
    {
        isOn = true;
        timeUntilOff = Random.Range(3f, 10f);
        spriteRenderer = GetComponent<SpriteRenderer>();

        ToggleLight(isOn);
    }

    void Update()
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
            if (isOn) timeUntilOff = Random.Range(3f, 10f);
            ToggleLight(isOn);
        }

        if (isClickInput) ClickVariant();
        
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
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonUp(1))
        {
            isOn = !isOn;
            if (isOn) timeUntilOff = Random.Range(3f, 10f);
            ToggleLight(isOn);
        }
    }


}
