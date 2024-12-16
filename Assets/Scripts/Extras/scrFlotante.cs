using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFlotante : MonoBehaviour
{
    private bool subiendo = false;
    private float t = 0f;
    public bool isUI;
    public float minY;
    public float maxY;

    void Update()
    {
        // Adjust `t` value based on the direction
        if (subiendo)
        {
            t += Time.deltaTime; // Increase t
            if (t >= 1f) // Clamp t and reverse direction
            {
                t = 1f;
                subiendo = false;
            }
        }
        else
        {
            t -= Time.deltaTime; // Decrease t
            if (t <= 0f) // Clamp t and reverse direction
            {
                t = 0f;
                subiendo = true;
            }
        }

        // Smooth interpolation between minY and maxY

        if (!isUI)
        {
            float newY = Mathf.SmoothStep(minY, maxY, t);
            transform.position = new Vector2(transform.position.x, newY);
        }
        else
        {
            float newY = Mathf.SmoothStep(minY, maxY, t);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(GetComponent<RectTransform>().anchoredPosition.x, newY);
        }
    }
}
