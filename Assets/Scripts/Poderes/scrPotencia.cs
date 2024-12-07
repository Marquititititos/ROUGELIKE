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

        if (arma.GetComponent<scrArmaBase>() != null)
        {
            arma.GetComponent<scrArmaBase>().da�o += 0.5f;
            if (arma.GetComponent<scrMetralleta>() == null)
            {
                arma.GetComponent<scrArmaBase>().cooldown -= 0.2f;
            }
            else
            {
                arma.GetComponent<scrArmaBase>().cooldown -= 0.03f;
            }
        } 

        if (arma.GetComponent<scrLanzallamas>() != null)
        {
            arma.GetComponent<scrLanzallamas>().da�o += 0.5f;
            arma.GetComponent<scrLanzallamas>().cooldown -= 0.2f;
        }

        if (arma.GetComponent<scrArmaL�ser>() != null)
        {
            arma.GetComponent<scrArmaL�ser>().da�o += 0.5f;
            arma.GetComponent<scrArmaL�ser>().cooldown -= 0.2f;
        }

        if (arma.GetComponent<scrCuchillo>() != null)
        {
            arma.GetComponent<scrCuchillo>().da�o += 0.5f;
            arma.GetComponent<scrCuchillo>().cooldown -= 0.2f;
        }

        GetComponentInParent<scrPlayer>().spd += 1;

    }

    private IEnumerator Duraci�n()
    {
        GameObject arma = GameObject.FindGameObjectWithTag("Arma");
        yield return new WaitForSeconds(cooldown);

        if(arma.GetComponent<scrArmaBase>() != null)
        {
            arma.GetComponent<scrArmaBase>().da�o -= 0.5f;
            if (arma.GetComponent<scrMetralleta>() == null)
            {
                arma.GetComponent<scrArmaBase>().cooldown += 0.2f;
            }
            else
            {
                arma.GetComponent<scrArmaBase>().cooldown += 0.03f;
            }
        }

        if (arma.GetComponent<scrLanzallamas>() != null)
        {
            arma.GetComponent<scrLanzallamas>().da�o -= 0.5f;
            arma.GetComponent<scrLanzallamas>().cooldown += 0.2f;
        }

        if (arma.GetComponent<scrArmaL�ser>() != null)
        {
            arma.GetComponent<scrArmaL�ser>().da�o -= 0.5f;
            arma.GetComponent<scrArmaL�ser>().cooldown += 0.2f;
        }

        if (arma.GetComponent<scrCuchillo>() != null)
        {
            arma.GetComponent<scrCuchillo>().da�o -= 0.5f;
            arma.GetComponent<scrCuchillo>().cooldown += 0.2f;
        }
        GetComponentInParent<scrPlayer>().spd -= 1;
        GetComponentInParent<scrPoderes>().StartCoroutine(GetComponentInParent<scrPoderes>().cooldown(cooldownReuso));
        Destroy(gameObject);
    }
}
