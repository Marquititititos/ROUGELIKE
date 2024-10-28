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
        } else
        {
            rb.velocity = new Vector2(direction.x, direction.y).normalized * spd;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ColisionExterna")
        {
            StartCoroutine(End());
        }

        if (collision.gameObject.tag == "Player")
        {
            if (sr.material = new Material(Shader.Find("Sprites/Default")))
            {
                StartCoroutine(End());
                scrPlayer scrplayer = collision.gameObject.GetComponent<scrPlayer>();
                scrplayer.StartCoroutine(scrplayer.Golpe(daño));
            }
        }
    }

    private IEnumerator Spawn()
    {
        sr.material = new Material(Shader.Find("GUI/Text Shader"));
        yield return new WaitForSeconds(0.2f);
        sr.material = new Material(Shader.Find("Sprites/Default"));
    }

    private IEnumerator End()
    {
        rb.velocity = new Vector2(0, 0);
        sr.material = new Material(Shader.Find("GUI/Text Shader"));
        while (sr.color.a > 0)
        {
            Color newColor = sr.color;
            newColor.a -= 0.005f;
            sr.color = newColor;
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
