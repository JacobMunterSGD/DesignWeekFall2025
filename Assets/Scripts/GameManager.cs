using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool gameOver;

    private void Awake()
    {
        Instance = this;
    }

    public void TriggerEndGameSequence()
    {
        print("end game sequence triggered");
    }
}
