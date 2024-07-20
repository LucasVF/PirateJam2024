using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public int playerLife = 3;
    // Start is called before the first frame update
    void Start()
    {
        ShowLife();
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
