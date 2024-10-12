using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBalaEscopeta : scrBalaBase
{
    public override void Direccion()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = camara.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        rb.velocity = new Vector2(direction.x + Random.Range(-1.5f, 1.5f), direction.y + Random.Range(-1.5f, 1.5f)).normalized * spd;
    }
}
