using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBala : MonoBehaviour
{
    // Variables

    public float spd;
    public float da�o;

    //Objetos 

    private Vector3 mousePos;
    private Camera camara;
    public Rigidbody2D rb;


    void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = camara.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * spd;

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Colision" || collision.gameObject.tag == "Enemigo") {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Enemigo")
        {
            scrEnemigo scriptEnemigo = collision.gameObject.GetComponent<scrEnemigo>();
            scriptEnemigo.hp -= da�o;
        }
    }
}