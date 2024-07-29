using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingEffect : MonoBehaviour
{
    public float speed = 0.5f;
    [SerializeField]
    Text _text;

    private void OnEnable()
    {
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        bool start = false;
        while (gameObject.activeSelf)
        {
            Color color = Color.black;
            color.a = !start ? 1 : 0;
            _text.color = color;
            yield return new WaitForSeconds(speed);
            start = !start;
        }
    }
}
