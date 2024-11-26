using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrOndaDeChoque : scrPoderBase
{
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Player").transform);

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2, enemigosLayer);
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<scrEnemigoBase>().StartCoroutine(hit.GetComponent<scrEnemigoBase>().Daño(daño));
            StartCoroutine(empuje(hit));
        }
    }

    private IEnumerator empuje(Collider2D hit)
    {
        if (hit != null)
        {
            hit.gameObject.GetComponent<Rigidbody2D>().AddForce((hit.gameObject.transform.position - transform.position).normalized * 5, ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0.2f);

        if (hit != null)
        {
            hit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            hit.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
