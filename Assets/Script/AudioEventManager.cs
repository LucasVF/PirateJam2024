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
    AudioSource CharacterFallingAudio;
    [SerializeField]
    AudioSource VictoryCommemorationAudio;

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
    public void TriggerCollectibleAudio() => TriggerAudio(CollectibleAudio);    
    public void TriggerCharacterFallingAudio() => TriggerAudio(CharacterFallingAudio);
    public void TriggerCharacterCommemorationAudio() => TriggerAudio(VictoryCommemorationAudio);
    public void TriggerGameplayThemeAudio()
    {
        ThemeAudio.volume = 0.15f;
        ChangeTheme(GameplayAudio);
    }

    public void TriggerTitleThemeAudio()
    {
        ThemeAudio.volume = 0.4f;
        ChangeTheme(TitleAudio);
    }

    public void TriggerResultThemeAudio(bool isWinner)
    {
        
        if (isWinner) {
            ThemeAudio.volume = 0.15f;
            ChangeTheme(VictoryAudio);
        } else {
            ThemeAudio.volume = 0.4f;
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

    public void StopTheme()
    {
        ThemeAudio.Stop();
    }

    public void ResumeTheme()
    {
        ThemeAudio.UnPause();
    }
}