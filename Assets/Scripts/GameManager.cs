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

    [SerializeField] Canvas basementCanvas;

    [SerializeField] string winText;

    private void Awake()
    {
        Instance = this;
        tutorialOver = false;
    }

    private void Start()
    {
        gameOverText.enabled = false;

        startText.enabled = true;

        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();

    }

    public void EndTutorial()
    {
        StartCoroutine(BeginGameAfterTutorial());
    }

    IEnumerator BeginGameAfterTutorial()
    {
        tutorialOver = true;
        LightManager.Instance.SetMedianStartTime(Time.time);

        startText.enabled = false;


        yield return new WaitForSeconds(1f);

        LightManager.Instance.FlickerAllLights();

        yield return new WaitForSeconds(1);

        AudioManager.Instance.Play(AudioManager.Instance.windowCreak, 1);
        AudioManager.Instance.StartAmbientNoise();

        LightManager.Instance.ToggleAllLights(false);


    }

    public void TriggerEndGameSequence(bool didPlayerWin)
    {
        gameOver = true;
        print("end game sequence triggered");

        AudioManager.Instance.EndAmbientNoise();

        StartCoroutine(EndGameSequence(didPlayerWin));
    }

    IEnumerator EndGameSequence(bool _didPlayerWin)
    {
        if (_didPlayerWin)
        {
            gameOverText.text = winText;
            gameOverText.enabled = true;
            AudioManager.Instance.Play(AudioManager.Instance.victorySFX, 1);
        }
        else
        {
            BasementImages.Instance.JumpScare();
            AudioManager.Instance.Play(AudioManager.Instance.jumpScareSFX, 1);

            basementCanvas.gameObject.SetActive(false);                  
        }

        yield return new WaitForSeconds(10);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
