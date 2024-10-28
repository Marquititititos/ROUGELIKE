using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFantasma : scrEnemigoBase
{
    public bool isColliding;


    public float maxSpd;
    public float minSpd;
    public float spd;

    public override void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.HSVToRGB(Random.Range(0, 0.99f), 0.38f, 1);
        spd = Random.Range(minSpd, maxSpd);

        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (isColliding)
        {
             scrplayer.StartCoroutine(scrplayer.Golpe(da�o));
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * spd);

        if (Vector2.Distance(transform.position, player.transform.position) < 0.1f)
        {
            canAttack = false;
        } else
        {
            canAttack = true;
        }
    } 
  

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            isColliding = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            isColliding = false;
        }
    }
}
