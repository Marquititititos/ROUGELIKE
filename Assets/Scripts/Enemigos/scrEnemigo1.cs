using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class scrEnemigo1 : scrEnemigoBase
{
    public GameObject ataque1;
    public Transform attackPoint;
    [HideInInspector] public NavMeshAgent agent;

    public override void Start()
    {
        base.Start();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = spd;
    }

    public override void Update()
    {
        base.Update();

        agent.SetDestination(player.transform.position);

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

        agent.speed = 0;
        canAttack = false;

        yield return new WaitForSeconds(2);

        animator.SetFloat("anim", 2);
        Collider2D[] playerC = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        Instantiate(ataque1, attackPoint.position, Quaternion.identity);

        if (playerC.Length > 0)
        {
            if (scrplayer.invincible == false)
            {
                scrplayer.StartCoroutine(scrplayer.Golpe(daño));
            }
        }


        yield return new WaitForSeconds(cooldown);

        agent.speed = spd;
        animator.SetFloat("anim", 0);
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
