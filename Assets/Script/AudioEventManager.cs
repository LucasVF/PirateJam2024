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
    public void TriggerTitleThemeAudio() => ChangeTheme(TitleAudio);
    public void TriggerVictoryThemeAudio() => ChangeTheme(VictoryAudio);
    public void TriggerLossThemeAudio() => ChangeTheme(LossAudio);

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