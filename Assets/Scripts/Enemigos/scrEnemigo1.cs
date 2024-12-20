using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class scrEnemigo1 : scrEnemigoBase
{
    public GameObject ataque1;
    public Transform attackPoint;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        Collider2D[] playerC = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        if (playerC.Length > 0)
        {
            if (canAttack)
            {
                animator.SetFloat("anim", 1);
                StartCoroutine(Atacar());
            }
        }

        if (canAttack)
        {
            animator.SetFloat("anim", 0);
        }
    }

    private IEnumerator Atacar()
    {
        isAttacking = true;
        GetComponent<scrPathFinding>().agent.speed = 0;
        canAttack = false;

        yield return new WaitForSeconds(1);

        animator.SetFloat("anim", 2);
        Collider2D[] playerC = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        Instantiate(ataque1, attackPoint.position, Quaternion.identity);

        if (playerC.Length > 0)
        {
             scrplayer.StartCoroutine(scrplayer.Golpe(da�o));
        }


        yield return new WaitForSeconds(cooldown);

        GetComponent<scrPathFinding>().agent.speed = spd;
        animator.SetFloat("anim", 0);
        canAttack = true;
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
