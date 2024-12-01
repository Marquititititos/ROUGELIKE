using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrBurbuja : scrPoderBase
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Duraci�n());
        transform.SetParent(GameObject.Find("Player").transform);
        GetComponentInParent<scrPlayer>().isBurbuja = true;
    }

    private IEnumerator Duraci�n()
    {
        yield return new WaitForSeconds(cooldown);
        GetComponentInParent<scrPlayer>().isBurbuja = false;
        GetComponentInParent<scrPlayer>().invincible = false;
        GetComponentInParent<scrPoderes>().StartCoroutine(GetComponentInParent<scrPoderes>().cooldown(cooldownReuso));
        Destroy(gameObject);
    }
}
