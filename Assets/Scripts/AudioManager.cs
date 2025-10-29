using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [Header("AudioSources")]
    public AudioSource EffectsSource;
    public AudioSource MusicSource;

    [Header("Music")]
    public AudioClip GameMusic;
    public AudioClip TitleMusic;

    [Header("Sound Effects")]
    public AudioClip exampleSFX;



    public Coroutine fadeMusic;


    void Awake()
    {
        if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

    private void Start()
    {
        //PlayMusic(GameMusic);
    }

    // Play a single clip through the sound effects source.
    public void Play(AudioClip clip)
    {
        AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
        tempAudioSource.clip = clip;
        tempAudioSource.Play();

        StartCoroutine(DestroyTempAudioSource(clip, tempAudioSource));
    }

    IEnumerator DestroyTempAudioSource(AudioClip clip, AudioSource audioSource)
    {
        yield return new WaitForSeconds(clip.length);

        Destroy(audioSource);

    }

    // Play a single clip through the music source.
    public void PlayMusic(AudioClip clip)
    {
        if (clip == GameMusic)
        {
            // make sure the game music loops
        }
        MusicSource.clip = clip;
        MusicSource.Play();
    }

    public IEnumerator FadeMusic(AudioClip SecondTrack, float fadeOutTime, float fadeInTime)
    {
        bool fadeIn = false;
        float currentVolume = MusicSource.volume;

        while (true)
        {
            if (!fadeIn)
            {
                if (currentVolume <= 0)
                {
                    PlayMusic(SecondTrack);
                    fadeIn = true;
                }
                currentVolume -= Time.deltaTime / fadeOutTime;
            }
            else
            {
                if (currentVolume >= 0.7)
                {
                    MusicSource.volume = 0.7f;
                    break;
                }
                currentVolume += Time.deltaTime / fadeInTime;

            }
            MusicSource.volume = currentVolume;
            yield return new WaitForEndOfFrame();
        }

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.J)) AudioManager.Instance.Play(AudioManager.Instance.exampleSFX);

    }


}