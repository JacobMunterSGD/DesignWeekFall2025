using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameOver;
    public bool tutorialOver;

    [SerializeField] TMP_Text gameOverText;
    [SerializeField] TMP_Text startText;

    private void Awake()
    {
        Instance = this;
        tutorialOver = false;
    }

    private void Start()
    {
        gameOverText.enabled = false;

        startText.enabled = true;

    }

    IEnumerator BeginGameAfterTutorial()
    {
        print("start real game");

        startText.enabled = false;

        // flicker animation

        yield return new WaitForSeconds(1);

    }

    public void EndTutorial()
    {
        tutorialOver = true;

        StartCoroutine(BeginGameAfterTutorial());
    }

    public void TriggerEndGameSequence()
    {
        gameOver = true;
        print("end game sequence triggered");
        gameOverText.enabled = true;
    }

    private void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            foreach(LightBulb light in LightManager.Instance.lightBulbs)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                //light.isOn = true;
                //light.ToggleLight(light.isOn);
                //DangerMeter.Instance.dangerSlider.value = 10;
                //gameOver = false;
            }
        }
    }
}
