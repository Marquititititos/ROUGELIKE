using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEscopeta : scrArmaBase
{
    public override void Disparar()
    {
        canShoot = false;

        for (int i = 0; i < 6; i++)
        {
            GameObject balaDisparada = Instantiate(bala, balaTransform.position, Quaternion.identity);
            balaDisparada.GetComponent<scrBalaBase>().daño = daño;
        }

        GameObject explo = Instantiate(explosion, balaTransform.position, Quaternion.identity);
        explo.transform.SetParent(this.transform);
    }
}
