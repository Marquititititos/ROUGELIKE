using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPuntero : MonoBehaviour
{
    void Update()
    {
        transform.position = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Cursor.visible = false;
    }
}
