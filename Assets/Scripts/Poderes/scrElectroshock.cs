using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrElectroshock : scrPoderBase
{
    public List<GameObject> enemigosEnZona = new List<GameObject>();
    public float duracion;
    public float cooldownZona = 0;
    private bool isShrinking = true;
    private bool hasEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Player").transform);
        transform.localScale = new Vector2(1, 1);
        StartCoroutine(Duración());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0,-0.2f,0);

        if (!hasEnded)
        {
            if (!isShrinking)
            {
                if (transform.localScale.x > 0.9f)
                {
                    transform.localScale = new Vector2(transform.localScale.x - 0.001f, transform.localScale.y - 0.001f);
                }
                else
                {
                    isShrinking = true;
                }
            }
            else
            {
                if (transform.localScale.x < 1.1f)
                {
                    transform.localScale = new Vector2(transform.localScale.x + 0.001f, transform.localScale.y + 0.001f);
                }
                else
                {
                    isShrinking = false;
                }
            }
        }

        if (enemigosEnZona.Count > 0)
        {
            if (cooldownZona < 0.5f)
            {
                cooldownZona += Time.deltaTime;
            }
            else
            {
                cooldownZona = 0;

                List<GameObject> enemigosParaDañar = new List<GameObject>(enemigosEnZona);

                if (enemigosParaDañar.Count > 0)
                {
                    foreach (GameObject enemigo in enemigosParaDañar)
                    {
                        if (enemigo.GetComponent<scrEnemigoBase>() != null)
                        {
                            if (enemigo != null)
                            {
                                enemigo.GetComponent<scrEnemigoBase>().StartCoroutine(enemigo.GetComponent<scrEnemigoBase>().Daño(daño));
                            }
                        }
                    }

                    enemigosEnZona.RemoveAll(e => e == null);
                }
            }
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            if (!enemigosEnZona.Contains(collision.gameObject))
            {
                enemigosEnZona.Add(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            if (enemigosEnZona.Contains(collision.gameObject))
            {
                enemigosEnZona.Remove(collision.gameObject);
            }
        }
    }

    private IEnumerator Duración()
    {
        yield return new WaitForSeconds(duracion);

        hasEnded = true;

        while(transform.localScale.x > 0)
        {
            transform.localScale = new Vector2(transform.localScale.x - 0.005f, transform.localScale.y - 0.005f);
            yield return null;
        }

        GetComponentInParent<scrPoderes>().StartCoroutine(GetComponentInParent<scrPoderes>().cooldown(cooldownReuso));

        Destroy(gameObject);
    }
}
