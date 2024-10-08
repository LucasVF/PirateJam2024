using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ViewType
{
    Start,
    Game,
    Result
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    View _startView;
    [SerializeField]
    GameView _gameView;
    [SerializeField]
    ResultView _resultView;

    ViewType _currentView;

    Dictionary<ViewType, View> _dictViewTheme;

    void Start()
    {
        ClearViews();

        _dictViewTheme = new Dictionary<ViewType, View>()
        {
            { ViewType.Start, _startView},
            { ViewType.Game, _gameView},
            { ViewType.Result, _resultView}
        };

        _currentView = ViewType.Start;
        _dictViewTheme[_currentView].DisplayView();
    }

    void ClearViews()
    {
        _gameView.HideView();
        _resultView.HideView();
        _startView.HideView();
    }

    public void GoToMainMenu()
    {
        _dictViewTheme[_currentView].HideView();
        _currentView = ViewType.Start;
        _dictViewTheme[_currentView].SetUpView();
        _dictViewTheme[_currentView].DisplayView();
    }

    public void StartGame()
    {
        _dictViewTheme[_currentView].HideView();
        _currentView = ViewType.Game;
        _dictViewTheme[_currentView].SetUpView();
        _dictViewTheme[_currentView].DisplayView();
    }

    public void EndGame(bool isWinner, float time = 0f)
    {
        _dictViewTheme[_currentView].HideView();
        _currentView = ViewType.Result;
        _resultView.SetWinner(isWinner);
        _resultView.SetTimeElapsed(isWinner, time);
        _dictViewTheme[_currentView].DisplayView();
    }

    public void StartLevel(LevelScriptableObject levelConfig)
    {
        _dictViewTheme[_currentView].HideView();
        _currentView = ViewType.Game;
        _gameView.SetLevelConfig(levelConfig);
        _dictViewTheme[_currentView].SetUpView();
        _dictViewTheme[_currentView].DisplayView();
    }
}
