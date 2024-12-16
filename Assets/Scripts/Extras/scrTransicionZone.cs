using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrTransicionZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            scrBotones.instance.CambioDeEscena("Game");
        }
    }
}
