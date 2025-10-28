using UnityEngine;

public class Light : MonoBehaviour
{
    public bool isOn;
    float timeUntilOff;

    SpriteRenderer spriteRenderer;

    void Start()
    {
        isOn = true;
        timeUntilOff = Random.Range(3f, 10f);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilOff -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyUp(KeyCode.Space))
        {
            isOn = !isOn;
            timeUntilOff = Random.Range(3f, 10f);
        }

        if (isOn == true)
        {
            spriteRenderer.color = Color.yellow;
        }
        if (isOn == false)
        {
            spriteRenderer.color = Color.black;
        }

        if (timeUntilOff <= 0)
        {
            isOn = false;
        }
    }
}
