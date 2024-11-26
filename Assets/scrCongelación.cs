using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scrCongelación : scrPoderBase
{
    public List<GameObject> enemigos = new List<GameObject>();
    public List<GameObject> hielos = new List<GameObject>();
    public GameObject hielo;
    public float cooldownHielo;

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject enemigo in GameObject.FindGameObjectsWithTag("Enemigo"))
        {
            enemigos.Add(enemigo);
        }

        StartCoroutine(Congelar());
    }

    private IEnumerator Congelar()
    {
        foreach (GameObject enemigo in enemigos)
        {
            GameObject hieloPuesto = Instantiate(hielo, enemigo.transform.position, Quaternion.identity);
            hieloPuesto.transform.SetParent(enemigo.transform);
            hielos.Add(hieloPuesto);

            enemigo.GetComponent<scrEnemigoBase>().StopAllCoroutines();
            enemigo.GetComponent<scrEnemigoBase>().canAttack = false;
            enemigo.GetComponent<scrEnemigoBase>().isFrozen = true;

            enemigo.GetComponent<Animator>().Rebind();
            enemigo.GetComponent<Animator>().speed = 0;
        }

        yield return new WaitForSeconds(cooldownHielo);

        foreach (GameObject enemigo in enemigos)
        {
            enemigo.GetComponent<scrEnemigoBase>().canAttack = true;
            enemigo.GetComponent<scrEnemigoBase>().isFrozen = false;

            enemigo.GetComponent<Animator>().speed = 1;

            if (enemigo.GetComponent<NavMeshAgent>() != null)
            {
                enemigo.GetComponent<NavMeshAgent>().isStopped = false;
                enemigo.GetComponent<NavMeshAgent>().speed = enemigo.GetComponent<scrEnemigoBase>().spd;
            }

            if (enemigo.GetComponent<scrMedusa>() != null)
            {
                enemigo.GetComponent<scrMedusa>().StartCoroutine(enemigo.GetComponent<scrMedusa>().Movimiento());
            }

            if (enemigo.GetComponent<scrRobot>() != null || enemigo.GetComponent<scrAraña>() != null || enemigo.GetComponent<scrCuervo>() != null || enemigo.GetComponent<scrOjo>() != null)
            {
                enemigo.GetComponent<scrEnemigoBase>().cooldown = Random.Range(enemigo.GetComponent<scrEnemigoBase>().minCooldown, enemigo.GetComponent<scrEnemigoBase>().maxCooldown);
            }
        }

        foreach(GameObject hielo in hielos)
        {
            Destroy(hielo);
        }

        Destroy(gameObject);
    }
}
