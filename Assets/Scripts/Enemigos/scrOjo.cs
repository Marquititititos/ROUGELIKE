using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scrOjo : scrEnemigoBase
{
    public GameObject zona;
    public LayerMask enemigosLayer;
    public GameObject curacion;
    private List<GameObject> particulas = new List<GameObject>();

    public override void Start()
    {
        base.Start();

        GetComponent<scrPathFinding>().agent.speed = spd;
        cooldown = Random.Range(minCooldown, maxCooldown);
    }

    public override void Update()
    {
        if (hp < 1)
        {
            foreach (GameObject particula in particulas)
            {
                if (particula != null)
                {
                    Destroy(particula);
                }
            }
        }

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
        Instantiate(zona, transform.position, Quaternion.identity);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 3, enemigosLayer);

        foreach(Collider2D hit in hits)
        {
            if (hit != null && hit.gameObject != gameObject)
            {
                hit.GetComponent<scrEnemigoBase>().hp++;
                GameObject cura = Instantiate(curacion, hit.transform.position, Quaternion.identity);
                particulas.Add(cura);
                cura.GetComponent<ParticleSystem>().Play();
                cura.transform.SetParent(hit.gameObject.transform);
                cura.transform.localScale = new Vector2(1, 1);
            }
        }

        yield return new WaitForSeconds(0.5f);

        foreach(GameObject particula in particulas)
        {
            if (particula != null)
            {
                particula.GetComponent<ParticleSystem>().Stop();
            }
        }

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject particula in particulas)
        {
            if (particula != null)
            {
                Destroy(particula);
            }
        }


        canAttack = true;
        cooldown = Random.Range(minCooldown, maxCooldown);
        yield return new WaitForSeconds(1);
        animator.SetFloat("anim", 0);
        yield return new WaitForSeconds(1);
        GetComponent<scrPathFinding>().agent.speed = spd;
    }
}
