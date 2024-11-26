using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scrMedusa : scrEnemigoBase
{
    public bool isColliding = false;

    public override void Start()
    {
        base.Start();

        StartCoroutine(Movimiento());

        canAttack = false;
    }

    public override void Update()
    {
        base.Update();

        if (isColliding)
        {
            scrplayer.StartCoroutine(scrplayer.Golpe(daño));
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

    public IEnumerator Movimiento()
    {
        GetComponent<scrPathFinding>().agent.speed = spd;
        GetComponent<scrPathFinding>().agent.isStopped = true;

        animator.SetFloat("anim", 1);
        yield return new WaitForSeconds(0.3f);

        animator.SetFloat("anim", 2);

        GetComponent<scrPathFinding>().agent.isStopped = false;
        while (GetComponent<scrPathFinding>().agent.speed > 0)
        {
            GetComponent<scrPathFinding>().agent.speed -= 0.01f;
            yield return null;
        }

        animator.SetFloat("anim", 0);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Movimiento());
    }
}