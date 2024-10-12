using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class scrBalaBase : MonoBehaviour
{
    // Variables

    public float spd;
    public float daño;

    //Objetos 

    [HideInInspector] public Vector3 mousePos;
    [HideInInspector] public Camera camara;
    public Rigidbody2D rb;
    public GameObject explosion;

    public abstract void Direccion();

    public virtual void Start()
    {
        Direccion();
    }

    // Update is called once per frame
    public virtual void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Colision" || collision.gameObject.tag == "Enemigo")
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Enemigo")
        {
            scrEnemigoBase scriptEnemigo = collision.gameObject.GetComponent<scrEnemigoBase>();
            scriptEnemigo.StartCoroutine(scriptEnemigo.Daño(daño));
            Destroy(this.gameObject);
        }
    }
}
