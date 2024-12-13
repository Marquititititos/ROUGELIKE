using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scrCuchillo : scrArmaBase
{
    private Vector3 mousePos;
    private Camera camara;
    public Animator anim;
    public LayerMask enemigosLayer;

    public override void Update()
    {
        base.Update();
    }

    public override void Disparar()
    {
        canShoot = false;

        GameObject ataque = Instantiate(bala, balaTransform.position, transform.rotation);
        ataque.transform.SetParent(gameObject.transform);

        Collider2D[] hits = Physics2D.OverlapCircleAll(balaTransform.position, 1.4f, enemigosLayer);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject != null)
            {
                if (hit.gameObject.GetComponent<Rigidbody2D>() != null)
                {
                    hit.gameObject.GetComponent<scrEnemigoBase>().StartCoroutine(hit.gameObject.GetComponent<scrEnemigoBase>().Daño(daño, true));

                    if (hit.gameObject.GetComponent<scrEnemigoBase>().isAttacking == false)
                    {
                        StartCoroutine(Ataque(hit));
                    }
                }
            }
        }
    }

    private IEnumerator Ataque(Collider2D hit)
    {
        if (hit.gameObject.GetComponent<scrEnemigoBase>() != null)
        {
            scrEnemigoBase screnemigo = hit.gameObject.GetComponent<scrEnemigoBase>();
            hit.gameObject.GetComponent<Rigidbody2D>().AddForce((hit.gameObject.transform.position - transform.position).normalized * 5, ForceMode2D.Impulse);

            float speed = screnemigo.spd;

            if (hit.gameObject.GetComponent<scrPathFinding>() == null)
            {
                screnemigo.spd = 0;
            }
        
            else
            {
                if (screnemigo.isFrozen == false)
                {
                    screnemigo.GetComponent<NavMeshAgent>().velocity = new Vector2(0,0);
                }
            }

            yield return new WaitForSeconds(0.3f);

            if (hit != null)
            {
                hit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                hit.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;

                if (hit.gameObject.GetComponent<scrPathFinding>() == null)
                {
                    screnemigo.spd = speed;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(balaTransform.position, 1.4f);
    }
}
