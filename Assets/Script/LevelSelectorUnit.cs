using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorUnit : MonoBehaviour
{
    [SerializeField]
    Text _text;
    LevelScriptableObject _levelConfig;
    LevelSelectorManager _manager;

    public void SetLevelConfig(LevelScriptableObject levelConfig, LevelSelectorManager manager)
    {
        _text.text = levelConfig.levelID.ToString();
        _levelConfig = levelConfig;
        _manager = manager;
    }

    public void StartLevel()
    {
        _manager.StartLevel(_levelConfig);
    }
}
