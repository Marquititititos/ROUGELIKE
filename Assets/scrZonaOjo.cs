using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrZonaOjo : MonoBehaviour
{
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.color.a > 0)
        {
            Color newColor = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.0005f);
            sr.color = newColor;
        } else
        {
            Destroy(gameObject);
        }
    }
}
