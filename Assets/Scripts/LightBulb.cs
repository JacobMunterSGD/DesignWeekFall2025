using System.Collections;
using UnityEngine;

public class LightBulb : MonoBehaviour
{
    public bool isOn;
    [SerializeField] float timeUntilOff;

    bool hasFlickered;
    Coroutine flickerCoroutine;

    SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    [SerializeField] Transform spriteHandle;
    Transform lightOn;
    //Transform lightOff;

    [Header("Input")]
    [SerializeField] KeyCode keyCode;
    [SerializeField] bool isClickInput;

    void Start()
    {
        isOn = false;
        timeUntilOff = GetNewCountdown();
        spriteRenderer = GetComponent<SpriteRenderer>();

        foreach (Transform t in spriteHandle)
        {
            if (t.name == "Light On") lightOn = t;
            //if (t.name == "Light Off") lightOff = t;
        }

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
        if (!GameManager.Instance.tutorialOver) return;

        if (timeUntilOff > 0) timeUntilOff -= Time.deltaTime;
        else
        {
            bool wasJustOn = isOn;
            isOn = false;
            if (wasJustOn) ToggleLight(isOn);
            return;
        }
        if (timeUntilOff < LightManager.Instance.countdownFlickerTime && !hasFlickered)
        {
            hasFlickered = true;
            Flicker();
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
        if (flickerCoroutine != null) StopCoroutine(flickerCoroutine);

        if (isTurningOn)
        {
            //spriteRenderer.color = Color.yellow;
            //spriteRenderer.sprite = LightManager.Instance.lightOn;
            hasFlickered = false;
            ToggleLightSprite(true);
            GetNewCountdown();
        }
        else
        {
            //spriteRenderer.color = Color.black;
            LightManager.Instance.LitLightCount();
            //spriteRenderer.sprite = LightManager.Instance.lightOff;
            hasFlickered = true;
            ToggleLightSprite(false);
            timeUntilOff = 0;
        }

        //print($"toggled button {gameObject.name}");

    }

    void ClickVariant()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {
            isOn = !isOn;
            if (isOn) timeUntilOff = Random.Range(3f, 10f);
            ToggleLight(isOn);
        }
        //print($"toggled button {gameObject.name}");
    }

    void ToggleLightSprite(bool isBeingTurnedOn)
    {
        lightOn.gameObject.SetActive(isBeingTurnedOn);
        //lightOff.gameObject.SetActive(!isBeingTurnedOn);
    }

    public void Flicker()
    {
        if (flickerCoroutine != null) StopCoroutine(flickerCoroutine);

        AudioManager.Instance.Play(AudioManager.Instance.lightFlicker, 1);

        flickerCoroutine = StartCoroutine(COFlicker());
    }

    IEnumerator COFlicker()
    {
        ToggleLightSprite(!isOn);

        yield return new WaitForSeconds(Random.Range(.03f, .06f));

        ToggleLightSprite(isOn);

        yield return new WaitForSeconds(Random.Range(.04f, .12f));

        ToggleLightSprite(!isOn);

        yield return new WaitForSeconds(Random.Range(.02f, .09f));

        ToggleLightSprite(isOn);

        yield return new WaitForSeconds(Random.Range(.02f, .09f));

        ToggleLightSprite(!isOn);

        yield return new WaitForSeconds(Random.Range(.02f, .09f));

        ToggleLightSprite(isOn);
    }


}
