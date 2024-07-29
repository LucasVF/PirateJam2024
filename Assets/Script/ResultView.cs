using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultView : View
{
    bool _isWinner = false;
    [SerializeField]
    Text _resultsText;
    [SerializeField]
    Text _timeText;

    [SerializeField]
    GameObject _playNextLevelButton;
    [SerializeField]
    GameObject _victoryComic;
    [SerializeField]
    GameObject _failureComic;
    [SerializeField]
    LevelSelectorManager _levelSelectorManager;

    public LifeManager LifeManager;
    public GameObject playerTopGameObject;
    private Vector3 playerOriginalScale = new Vector3(1f, 1f, 1f);

    public override void DisplayView()
    {
        AudioEventManager.Instance.TriggerResultThemeAudio(_isWinner);
        ChangeViewDisplay(true);
    }

    public override void SetUpView()
    {
        Debug.Log("Setting Up Result View");
    }

    public void SetWinner(bool isWinner)
    {
        _resultsText.text = isWinner ? "Yay! You are rich" : "Boohoo... You are poor =(";
        _isWinner = isWinner;
        _playNextLevelButton.SetActive(isWinner && !_levelSelectorManager.IsLastLevel());
        _victoryComic.SetActive(isWinner);
        _failureComic.SetActive(!isWinner);

        LifeManager.playerLife = 3;
        playerTopGameObject.transform.localScale = playerOriginalScale;
    }

    public void SetTimeElapsed(bool isWinner, float time)
    {
        _timeText.text = isWinner ? "Time Elapsed: " + FormatTime(time) : "";
    }
    string FormatTime(float timeInSeconds)
    {
        int hours = Mathf.FloorToInt(timeInSeconds / 3600);
        int minutes = Mathf.FloorToInt((timeInSeconds % 3600) / 60);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60);

        if (hours > 0)
        {
            return string.Format("{0:D2}:{1:D2}:{2:D2}s", hours, minutes, seconds);
        }
        else if (minutes > 0)
        {
            return string.Format("{0:D2}:{1:D2}s", minutes, seconds);
        }
        else
        {
            return string.Format("{0:D2}s", seconds);
        }
    }
}
