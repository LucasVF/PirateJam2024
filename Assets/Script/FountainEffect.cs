using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFountainEffect : MonoBehaviour
{
    public Sprite imageSprite; // The sprite to be used as water
    public Vector3 originPoint; // The origin point of the fountain
    public float heightBound = 200f; // The maximum height the image can reach (in UI coordinates)
    public float widthBound = 50f; // The horizontal spread of the fountain (in UI coordinates)
    public float spawnInterval = 0.1f; // Interval between each image spawn
    public float imageSpeed = 50f; // Speed at which the images move upwards (in UI coordinates)
    public int poolSize = 50; // Number of images in the pool

    private List<GameObject> pool = new List<GameObject>();
    private RectTransform canvasRectTransform;

    void OnEnable()
    {
        ResetPool();
        // Get the RectTransform of the parent Canvas
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

        // Initialize the object pool with the Image prefab
        InitializePool();

        // Start generating the fountain
        StartCoroutine(GenerateFountain());
    }

    void InitializePool()
    {
        pool = new List<GameObject>();
        GameObject imagePrefab = new GameObject("FountainImage");
        Image img = imagePrefab.AddComponent<Image>();
        img.sprite = imageSprite;
        imagePrefab.GetComponent<RectTransform>().sizeDelta = new Vector2(50, 49);
        imagePrefab.SetActive(false);

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(imagePrefab, canvasRectTransform);
            obj.transform.parent = transform;
            obj.SetActive(false);
            pool.Add(obj);
        }

        Destroy(imagePrefab); // Clean up the temporary prefab object
    }

    void ResetPool()
    {
        if (pool.Count > 0)
        {
            foreach (GameObject unit in pool)
            {
                unit.SetActive(false);
            }
        }
    }

    GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }

        // Optionally, expand the pool if all objects are in use
        GameObject newObj = Instantiate(pool[0], canvasRectTransform); // Instantiate a copy of the first pooled object
        newObj.SetActive(false);
        newObj.transform.parent = transform;
        pool.Add(newObj);
        return newObj;
    }

    IEnumerator GenerateFountain()
    {
        while (true)
        {
            // Get an image from the pool and activate it
            GameObject imageObj = GetPooledObject();
            imageObj.transform.localPosition = originPoint;
            imageObj.SetActive(true);

            // Move the image upwards
            StartCoroutine(MoveImage(imageObj));

            // Wait for the next spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator MoveImage(GameObject imageObj)
    {
        RectTransform rectTransform = imageObj.GetComponent<RectTransform>();
        
        while (rectTransform.localPosition.y < originPoint.y + heightBound)
        {
            // Move the image upwards with a slight random horizontal offset
            float offsetX = Random.Range(-widthBound, widthBound);
            Vector3 newPosition = rectTransform.localPosition + new Vector3(offsetX, imageSpeed * Time.deltaTime, 0);
            rectTransform.localPosition = newPosition;

            yield return null;
        }

        // Deactivate the image once it goes out of bounds
        imageObj.SetActive(false);
    }
}
