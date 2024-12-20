using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrFuego : MonoBehaviour
{
    public GameObject lanzallamas;
    public List<GameObject> enemigosEnFuego = new List<GameObject>();
    public float cooldown = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemigosEnFuego.Count > 0)
        {
            if (cooldown < GetComponentInParent<scrLanzallamas>().cooldown)
            {
                cooldown += Time.deltaTime;
            } else
            {
                cooldown = 0;

                List<GameObject> enemigosParaDaņar = new List<GameObject>(enemigosEnFuego);

                if (enemigosParaDaņar.Count > 0)
                {
                    foreach (GameObject enemigo in enemigosParaDaņar)
                    {
                        if (enemigo != null)
                        {
                            if (enemigo.GetComponent<scrEnemigoBase>() != null)
                            {
                                enemigo.GetComponent<scrEnemigoBase>().StartCoroutine(enemigo.GetComponent<scrEnemigoBase>().Daņo(lanzallamas.GetComponent<scrLanzallamas>().daņo));
                            }
                        }
                    }

                    enemigosEnFuego.RemoveAll(e => e == null);
                }
            }
        } else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            if (!enemigosEnFuego.Contains(collision.gameObject))
            {
                enemigosEnFuego.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            if (enemigosEnFuego.Contains(collision.gameObject))
            {
                enemigosEnFuego.Remove(collision.gameObject);
            }
        }
    }
}
