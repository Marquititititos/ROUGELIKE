using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBackground : MonoBehaviour
{
    public Sprite[] spritesBG1;
    public Sprite[] spritesBG2;
    public GameObject[] BG1;
    public GameObject[] BG2;
    public float speed1;
    public float speed2;
    public int fondo;

    // Update is called once per frame
    private void Update()
    {
        foreach (GameObject bg in BG1)
        {
            bg.GetComponent<SpriteRenderer>().sprite = spritesBG1[fondo];
            bg.transform.position = new Vector2(bg.transform.position.x - speed1 * Time.deltaTime, bg.transform.position.y);
            if (bg.transform.position.x < -15)
            {
                bg.transform.position = new Vector2(24.5f, bg.transform.position.y);
            }
        }

        foreach (GameObject bg in BG2)
        {
            bg.GetComponent<SpriteRenderer>().sprite = spritesBG2[fondo];
            bg.transform.position = new Vector2(bg.transform.position.x - speed2 * Time.deltaTime, bg.transform.position.y);
            if (bg.transform.position.x < -15)
            {
                bg.transform.position = new Vector2(24.5f, bg.transform.position.y);
            }
        }
    }
}
