using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrAraña : scrEnemigoBase
{
    public Transform attackPoint;
    public GameObject bola;
    public GameObject impacto;

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
        isAttacking = true;
        GetComponent<scrPathFinding>().agent.speed = 0;
        canAttack = false;
        animator.SetFloat("anim", 1);
        yield return new WaitForSeconds(1.5f);
        GameObject impact = Instantiate(impacto, attackPoint.position, Quaternion.identity);
        GameObject misil = Instantiate(bola, attackPoint.position, Quaternion.identity);
        misil.GetComponent<scrMisilAraña>().daño = daño;
        animator.SetFloat("anim", 2);
        canAttack = true;
        cooldown = Random.Range(minCooldown, maxCooldown);
        yield return new WaitForSeconds(0.5f);
        GetComponent<scrPathFinding>().agent.speed = spd;
        animator.SetFloat("anim", 0);
        isAttacking = false;
    }
}
