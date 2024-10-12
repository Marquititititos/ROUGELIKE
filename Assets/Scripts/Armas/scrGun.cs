using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGun : scrArmaBase
{

    public override void Disparar()
    {
        canShoot = false;
        GameObject balaDisparada = Instantiate(bala, balaTransform.position, Quaternion.identity);
        balaDisparada.GetComponent<scrBala>().da�o = da�o;
        GameObject explo = Instantiate(explosion, balaTransform.position, Quaternion.identity);
        explo.transform.SetParent(this.transform);
    }
}
