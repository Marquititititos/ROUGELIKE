using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFuncionesBotones : MonoBehaviour
{
    public void Pausa()
    {
        scrBotones.instance.Pausa();
    }

    public void Cambiar(string escena)
    {
        if (escena != "Tutorial" || escena == "Tutorial" && scrBotones.instance.isPaused == false)
        {
            scrBotones.instance.CambioDeEscena(escena);
        }
    }
}
