using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scrCuervo : scrEnemigoBase
{
    public Transform attackPoint;
    public GameObject bola;

    public override void Start()
    {
        base.Start();

        cooldown = Random.Range(minCooldown, maxCooldown);
    }

    public override void Update()
    {
        base.Update();

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            if (canAttack)
            {
                StartCoroutine(Ataque());
            }
        }
    }

    private IEnumerator Ataque()
    {
        GetComponent<scrPathFinding>().agent.speed = 0;
        canAttack = false;
        animator.SetFloat("anim", 1);
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < 5; i++)
        {
            Instantiate(bola, attackPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(0.3f);
        }
        canAttack = true;
        cooldown = Random.Range(minCooldown, maxCooldown);
        yield return new WaitForSeconds(0.5f);
        GetComponent<scrPathFinding>().agent.speed = spd;
        animator.SetFloat("anim", 0);
    }
}
