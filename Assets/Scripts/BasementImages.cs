using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BasementImages : MonoBehaviour
{

    public static BasementImages Instance;

    [SerializeField] List<SpriteRenderer> allBackgroundImages;

    [SerializeField] SpriteRenderer tutorialBG;
    [SerializeField] SpriteRenderer BG1;
    [SerializeField] SpriteRenderer BG2;
    [SerializeField] SpriteRenderer BG3;
    [SerializeField] SpriteRenderer BG4;
    [SerializeField] SpriteRenderer BG5;
    [SerializeField] SpriteRenderer BG6;
    [SerializeField] SpriteRenderer BG7;
    [SerializeField] SpriteRenderer jumpscare;

    [SerializeField] SpriteRenderer blackScreen;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        blackScreen.enabled = false;
        foreach (Transform child in transform)
        {
            allBackgroundImages.Add(child.GetComponent<SpriteRenderer>());
        }
    }

    private void Update()
    {
        if (GameManager.Instance.gameOver)
        {
            ChangeBackground(jumpscare);
            return;
        }

        if (!GameManager.Instance.tutorialOver) ChangeBackground(tutorialBG);

        switch (DangerMeter.Instance.dangerValue / 20)
        {
            case < .14f:
                ChangeBackground(BG7);
                break;
            case > .14f and < .28f:
                ChangeBackground(BG6);
                break;
            case > .28f and < .42f:
                ChangeBackground(BG5);
                break;
            case > .42f and < .56f:
                ChangeBackground(BG4);
                break;
            case > .56f and < .70f:
                ChangeBackground(BG3);
                break;
            case > .70f and < .84f:
                ChangeBackground(BG2);
                break;
            case > .84f and < 1f:
                ChangeBackground(BG1);
                break;
        }
    }

    void ChangeBackground(SpriteRenderer activeBG)
    {  
        if (activeBG.enabled == true) return;

        foreach(SpriteRenderer sp in allBackgroundImages)
        {
            sp.enabled = false;
            sp.gameObject.SetActive(false);
        }

        StartCoroutine(BlackFlash(activeBG));
    }

    IEnumerator BlackFlash(SpriteRenderer activeBG)
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.enabled = true;

        yield return new WaitForSeconds(.08f);

        activeBG.enabled = true;
        activeBG.gameObject.SetActive(true);

        yield return new WaitForSeconds(.08f);

        blackScreen.gameObject.SetActive(false);
        blackScreen.enabled = false;
    }

}