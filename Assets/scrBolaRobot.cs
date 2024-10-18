using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBolaRobot : MonoBehaviour
{
    // Variables

    public float spd;
    public float daño;

    //Objetos 

    [HideInInspector] public Vector3 mousePos;
    [HideInInspector] public Camera camara;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector3 direction = GameObject.Find("Player").transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * spd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColisionExterna")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            scrPlayer scrplayer = collision.gameObject.GetComponent<scrPlayer>();
            scrplayer.StartCoroutine(scrplayer.Golpe(daño));
            Destroy(this.gameObject);
        }
    }
}
