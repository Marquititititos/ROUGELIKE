using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPotencia : scrPoderBase
{
    // Start is called before the first frame update
    void Start()
    {

        GameObject arma = GameObject.FindGameObjectWithTag("Arma");
        StartCoroutine(Duraci�n());
        transform.SetParent(GameObject.Find("Player").transform);
        arma.GetComponent<scrArmaBase>().da�o += 0.5f;
        arma.GetComponent<scrArmaBase>().cooldown -= 0.2f;
        GetComponentInParent<scrPlayer>().spd += 1;
    }

    private IEnumerator Duraci�n()
    {

        GameObject arma = GameObject.FindGameObjectWithTag("Arma");
        yield return new WaitForSeconds(cooldown);
        arma.GetComponent<scrArmaBase>().da�o -= 0.5f;
        arma.GetComponent<scrArmaBase>().cooldown += 0.2f;
        GetComponentInParent<scrPlayer>().spd -= 1;
        GetComponentInParent<scrPoderes>().StartCoroutine(GetComponentInParent<scrPoderes>().cooldown(cooldownReuso));
        Destroy(gameObject);
    }
}