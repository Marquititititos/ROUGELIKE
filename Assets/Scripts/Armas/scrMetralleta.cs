using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrMetralleta : scrArmaBase
{
    public override void Disparar()
    {
        canShoot = false;
        GameObject balaDisparada = Instantiate(bala, balaTransform.position, Quaternion.identity);
        balaDisparada.GetComponent<scrBala>().daño = daño;
    }

    private void Update()
    {
        base.Update();
        explosion.SetActive(!canShoot);
    }
}
