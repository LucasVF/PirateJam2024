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
       // Destroy(normalPlayer); // Commented for safety
        //normalPlayer.SetActive(false);
        GameObject shadowPlayer = GameObject.Find("PlayerBottom");
        //Destroy(shadowPlayer); // Disable Game Object!!
        //shadowPlayer.SetActive(false);
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");
        GameManager gameManagerComponent = gameManager.GetComponent<GameManager>();
        gameManagerComponent.EndGame(false);

        

        TranslocatePlayerToOriginalPosition(normalPlayer);
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

    private void TranslocatePlayerToOriginalPosition(GameObject playerTopGameObject)
    {
        playerTopGameObject.transform.position = new Vector3(-8f, 0.9f, playerTopGameObject.transform.position.z);
        playerLife = 3;
    }
}
