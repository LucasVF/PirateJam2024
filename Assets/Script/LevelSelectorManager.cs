using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorManager : MonoBehaviour
{
    [SerializeField]
    GameManager _gameManager;
    [SerializeField]
    GameObject _levelSelectorUnitPrefab;
    [SerializeField]
    List<LevelScriptableObject> _levelConfigs;
    List<GameObject> _levelList = new List<GameObject>();

    private void Start()
    {
        foreach (LevelScriptableObject levelConfig in _levelConfigs)
        {
            GameObject newLevel = Instantiate(_levelSelectorUnitPrefab, transform);
            newLevel.transform.position = transform.position;
            LevelSelectorUnit levelSelectorUnit = newLevel.GetComponent<LevelSelectorUnit>();
            levelSelectorUnit.SetLevelConfig(levelConfig, this);
            _levelList.Add(newLevel);
        }
        UpdateCarousel();
    }

    public void StartLevel(LevelScriptableObject levelConfig)
    {
        _gameManager.StartLevel(levelConfig);
    }

    public void StartNextLevel()
    {
        if (currentIndex + 1 < _levelConfigs.Count)
        {
            currentIndex++;
            _gameManager.StartLevel(_levelConfigs[currentIndex]);
        }        
    }

    public bool IsLastLevel()
    {
        return currentIndex > _levelList.Count - 2;
    }

    public float spacing = 400f; // Spacing between buttons
    public float transitionDuration = 0.5f; // Duration of the smooth transition
    private int currentIndex = 0;
    private bool isTransitioning = false;
    private float transitionProgress = 0f;
    private Vector3[] startPositions;
    private Vector3[] endPositions;
    [SerializeField]
    GameObject _moveRight;
    [SerializeField]
    GameObject _moveLeft;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }

        if (isTransitioning)
        {
            transitionProgress += Time.deltaTime / transitionDuration;
            for (int i = 0; i < _levelList.Count; i++)
            {
                _levelList[i].transform.localPosition = Vector3.Lerp(startPositions[i], endPositions[i], transitionProgress);
            }

            if (transitionProgress >= 1f)
            {
                isTransitioning = false;
                DisplayButtons();
                EnableCenterButton();
            }
        }
    }

    private void OnEnable()
    {
        UpdateCarousel();
    }

    public void MoveLeft()
    {
        if (currentIndex > 0 && !isTransitioning)
        {
            HideButtons();
            currentIndex--;
            StartTransition();
        }
    }

    public void MoveRight()
    {
        if (currentIndex < _levelList.Count - 1 && !isTransitioning)
        {
            HideButtons();
            currentIndex++;
            StartTransition();
        }
    }

    void HideButtons()
    {
        _moveRight.SetActive(false);
        _moveLeft.SetActive(false);
    }

    void DisplayButtons()
    {
        _moveRight.SetActive(true);
        _moveLeft.SetActive(true);
    }

    void StartTransition()
    {
        isTransitioning = true;
        transitionProgress = 0f;
        startPositions = new Vector3[_levelList.Count];
        endPositions = new Vector3[_levelList.Count];

        for (int i = 0; i < _levelList.Count; i++)
        {
            startPositions[i] = _levelList[i].transform.localPosition;
            float xPos = (i - currentIndex) * spacing;
            endPositions[i] = new Vector3(xPos, 0, 0);
        }
    }

    void UpdateCarousel()
    {
        for (int i = 0; i < _levelList.Count; i++)
        {
            float xPos = (i - currentIndex) * spacing;
            _levelList[i].transform.localPosition = new Vector3(xPos, 0, 0);
        }
        EnableCenterButton();
    }

    void EnableCenterButton()
    {
        for (int i = 0; i < _levelList.Count; i++)
        {
            Button buttonComponent = _levelList[i].GetComponent<Button>();
            Text textComponent = _levelList[i].GetComponentInChildren<Text>();
            if (i == currentIndex)
            {
                buttonComponent.interactable = true;
                if (textComponent != null)
                {
                    Color textColor = textComponent.color;
                    textColor.a = 1f; // Fully opaque
                    textComponent.color = textColor;
                }
            }
            else
            {
                buttonComponent.interactable = false;
                if (textComponent != null)
                {
                    Color textColor = textComponent.color;
                    textColor.a = 0.5f; // Semi-transparent
                    textComponent.color = textColor;
                }
            }
        }
    }
}
