using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventManager : MonoBehaviour
{
    public static AudioEventManager Instance { get; private set; }

    [SerializeField]
    AudioSource ButtonAudio;
    [SerializeField]
    AudioSource ThemeAudio;
    [SerializeField]
    AudioSource CollectibleAudio;

    [SerializeField]
    AudioClip GameplayAudio;
    [SerializeField]
    AudioClip TitleAudio;
    [SerializeField]
    AudioClip LossAudio;
    [SerializeField]
    AudioClip VictoryAudio;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerButtonAudio() => TriggerAudio(ButtonAudio);
    public void TriggerGameplayThemeAudio() => ChangeTheme(GameplayAudio);
    public void TriggerCollectibleAudio() => TriggerAudio(CollectibleAudio);
    public void TriggerTitleThemeAudio() => ChangeTheme(TitleAudio);
    public void TriggerResultThemeAudio(bool isWinner)
    {
        if (isWinner) {
            ChangeTheme(VictoryAudio);
        } else {
            ChangeTheme(LossAudio);
        }
    }

    private void TriggerAudio(AudioSource audioToPlay)
    {
        audioToPlay.Play();
    }

    public void ChangeTheme(AudioClip newTheme)
    {
        ThemeAudio.Stop();
        ThemeAudio.clip = newTheme;
        ThemeAudio.Play();
    }
}