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
            _text.enabled = !start;
            yield return new WaitForSeconds(speed);
            start = !start;
        }
    }
}
