using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class scrRoomSpawner : MonoBehaviour
{
    //Variables

    public float gridWidth;
    public float gridHeight;
    public float objectNumber;
    public float enemyNumber;

    //Objetos

    public List<GameObject> bloques;
    public List<GameObject> enemigos = new List<GameObject>();
    public GameObject enviroment;
    public List<Vector2> gridObjetos = new List<Vector2>();
    public List<Vector2> gridEnemigos = new List<Vector2>();
    public List<GameObject> instancias = new List<GameObject>();
    public ParticleSystem enemyIndicator;
    public List<ParticleSystem> advertencias = new List<ParticleSystem>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnear());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator Spawnear()
    {

        GameObject.Find("Player").GetComponent<scrPlayer>().isBurbuja = false;
        GameObject.Find("Player").GetComponent<scrPlayer>().invincible = false;
        GameObject.Find("Player").GetComponent<scrPoderes>().StopAllCoroutines();

        gridObjetos.Clear();
        gridEnemigos.Clear();
        instancias.Clear();
        advertencias.Clear();

        //Crear grid objetos
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                gridObjetos.Add(new Vector2(x - 7.5f, y - 5.5f));
            }
        }

        //Crear grid enemigos
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                gridEnemigos.Add(new Vector2(x - 7.5f, y - 5.5f));
            }
        }

        Debug.Log("GridEnemigos Count: " + gridEnemigos.Count);

        //Spawnear objetos
        for (int i = 0; i < objectNumber; i++)
        {
            if (gridObjetos.Count > 0)
            {
                GameObject objeto = Instantiate(bloques[Random.Range(0, bloques.Count)]);
                objeto.transform.SetParent(enviroment.transform);
                instancias.Add(objeto);
                objeto.transform.position = gridObjetos[Random.Range(0, gridObjetos.Count)];
                Vector2 vectorObjeto = objeto.transform.position;

                Vector2 minBound = vectorObjeto - new Vector2(1, 1);
                Vector2 maxBound = vectorObjeto + new Vector2(1, 1);

                gridObjetos.RemoveAll(v => v.x >= minBound.x && v.x <= maxBound.x && v.y >= minBound.y && v.y <= maxBound.y);
                gridEnemigos.Remove(vectorObjeto);
            }
        }

        yield return new WaitForSeconds(1);

        //Spawnear enemigos

        for (int i = 0; i < enemyNumber; i++)
        {
            ParticleSystem advertencia = Instantiate(enemyIndicator);
            advertencia.transform.position = gridEnemigos[Random.Range(0, gridEnemigos.Count)];
            Vector2 vectorEnemigo = advertencia.transform.position;
            gridEnemigos.Remove(vectorEnemigo);
            advertencias.Add(advertencia);
        }

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < advertencias.Count; i++)
        {
            GameObject enemigo = Instantiate(enemigos[Random.Range(0, enemigos.Count)]);
            instancias.Add(enemigo);

            if (enemigo.GetComponent<NavMeshAgent>() != null)
            {
                enemigo.GetComponent<NavMeshAgent>().enabled = false;
            }

            enemigo.transform.position = advertencias[i].gameObject.transform.position;
            Instantiate(enemigo.GetComponent<scrEnemigoBase>().explosionMuerte, enemigo.transform.position, Quaternion.identity);

            if (enemigo.GetComponent<NavMeshAgent>() != null)
            {
                enemigo.GetComponent<NavMeshAgent>().enabled = true;
            }

            Vector2 vectorEnemigo = enemigo.transform.position;
            gridEnemigos.Remove(vectorEnemigo);
            Destroy(advertencias[i].gameObject);
        }

        //foreach(Vector2 vector in gridEnemigos)
        //{
        //    GameObject d = Instantiate(aa);
        //    d.transform.position = vector;
        //    instancias.Add(d);
        //}
    }
}
