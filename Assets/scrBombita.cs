using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBombita : scrEnemigoBase
{

    public GameObject explosion; 

    public override void Start()
    {
        base.Start();

        GetComponent<scrPathFinding>().agent.speed = spd;
    }

    public override void Update()
    {
        base.Update();

        if (hp < 1)
        {
            explotar();
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            if (scrplayer.isDashing == false)
            {
                explotar();
            }
        }
    }

    private void explotar()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Collider2D[] playerC = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        if (playerC.Length > 0)
        {
            scrplayer.StartCoroutine(scrplayer.Golpe(daño));
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
