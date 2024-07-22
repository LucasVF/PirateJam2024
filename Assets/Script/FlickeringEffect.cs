using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlickeringEffect : MonoBehaviour
{
    public float speed = 0.1f;
    [SerializeField]
    Image _image;

    private void OnEnable()
    {
        StartCoroutine("Flink");
    }

    IEnumerator Flink()
    {
        bool start = false;
        while (gameObject.activeSelf)
        {
            _image.color = start ? Color.white : Color.white*0.95f;
            yield return new WaitForSeconds(speed);
            start = !start;
        }
    }
}
