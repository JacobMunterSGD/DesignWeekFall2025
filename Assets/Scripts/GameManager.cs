using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameOver;

    [SerializeField] TMP_Text gameOverText;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameOverText.enabled = false;
    }

    public void TriggerEndGameSequence()
    {
        gameOver = true;
        print("end game sequence triggered");
        gameOverText.enabled = true;
    }
}
