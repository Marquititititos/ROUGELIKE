using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMisilAraña : MonoBehaviour
{
    // Variables

    public float spd;
    public float daño;
    private bool isFollowing = true;
    private float cooldown = 0;

    //Objetos 

    [HideInInspector] public Vector3 mousePos;
    [HideInInspector] public Camera camara;
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Vector3 direction;
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (cooldown < 6)
        {
            cooldown += Time.deltaTime;
        } else
        {
            isFollowing = false;
        }

        if (isFollowing)
        {
            direction = GameObject.Find("Player").transform.position - transform.position;
            transform.position = Vector2.MoveTowards(transform.position, GameObject.Find("Player").transform.position, Time.deltaTime * spd);

            Vector3 rotation = transform.position - GameObject.Find("Player").transform.position;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rot + 90);

        } else
        {
            rb.velocity = new Vector2(direction.x, direction.y).normalized * spd;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColisionExterna")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            GameObject explo = Instantiate(explosion);
            explo.transform.position = transform.position;
            scrPlayer scrplayer = collision.gameObject.GetComponent<scrPlayer>();
            scrplayer.StartCoroutine(scrplayer.Golpe(daño));
            Destroy(this.gameObject);
        }
    }
}
