using UnityEngine;
using UnityEngine.UI;

public class WinClock : MonoBehaviour
{
    public static WinClock Instance;

    public float timeToWin;
    [SerializeField] Slider winSlider;

    [SerializeField] RectTransform clockHandHandle;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        winSlider.maxValue = timeToWin;
        winSlider.value = 0;

        clockHandHandle.rotation = Quaternion.Euler(0, 0, 90);
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver || !GameManager.Instance.tutorialOver) return;

        if (winSlider.value == winSlider.maxValue)
        {
            // you win
            GameManager.Instance.TriggerEndGameSequence(true);
            print("you win!");

        }
        else winSlider.value += Time.deltaTime;

        //print(-(winSlider.value * 2 - 90) + " " + winSlider.value) ;

        clockHandHandle.rotation = Quaternion.Euler(0, 0, -(winSlider.value * 2 - 90));

    }

}
