using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeManager : MonoBehaviour
{
    public int playerLife = 3;

    public Image[] hearts;
    public Sprite emptyHeartSprite;
    public Sprite fullHeartSprite;

    // Start is called before the first frame update
    void Start()
    {
        ShowLife();
    }

    // Update is called once per frame
    void Update()
    {
        SetupEmptyHeart();

        FillHeartBasedOnPlayerLife();
    }

    public void ShowLife()
    {
        Debug.Log($"Life: {playerLife}");
    }

    public void TakeDamage(int value)
    {
        playerLife -= value;
        Debug.Log($"{value} damage taken.");
        this. ShowLife();
    }

    public void DestroyPlayer()
    {
        GameObject normalPlayer = GameObject.Find("PlayerTop");
        Destroy(normalPlayer);
        GameObject shadowPlayer = GameObject.Find("PlayerBottom");
        Destroy(shadowPlayer);
    }

    public void SetupEmptyHeart()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeartSprite;
        }
    }

    public void FillHeartBasedOnPlayerLife()
    {
        for (int i = 0; i < playerLife; i++)
        {
            hearts[i].sprite = fullHeartSprite;
        }
    }
}
