using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEfectoCongelación : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<SpriteRenderer>().color.a > 0)
        {
            Color newColor = GetComponent<SpriteRenderer>().color;
            newColor.a -= 0.002f;
            GetComponent<SpriteRenderer>().color = newColor;
        } else
        {
            Destroy(gameObject);
        }
    }
}
