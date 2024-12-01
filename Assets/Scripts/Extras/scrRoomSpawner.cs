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

    public GameObject bloque;
    public List<GameObject> enemigos = new List<GameObject>();
    public GameObject enviroment;
    public GameObject aa;
    public List<Vector2> gridObjetos = new List<Vector2>();
    public List<Vector2> gridEnemigos = new List<Vector2>();
    public List<GameObject> instancias = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        Spawnear();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawnear()
    {

        GameObject.Find("Player").GetComponent<scrPlayer>().isBurbuja = false;
        GameObject.Find("Player").GetComponent<scrPlayer>().invincible = false;
        GameObject.Find("Player").GetComponent<scrPoderes>().StopAllCoroutines();
        GameObject.Find("Player").GetComponent<scrPoderes>().canPoder = true;

        gridObjetos.Clear();
        gridEnemigos.Clear();
        instancias.Clear();

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
                GameObject objeto = Instantiate(bloque);
                objeto.transform.SetParent(enviroment.transform);
                instancias.Add(objeto);
                objeto.transform.position = gridObjetos[Random.Range(0, gridObjetos.Count)];
                Vector2 vectorObjeto = objeto.transform.position;

                Vector2 minBound = vectorObjeto - new Vector2(1, 1);
                Vector2 maxBound = vectorObjeto + new Vector2(1, 1);

                gridObjetos.RemoveAll(v => v.x >= minBound.x && v.x <= maxBound.x && v.y >= minBound.y && v.y <= maxBound.y);
            }
        }

        //Spawnear enemigos
        for (int i = 0; i < enemyNumber; i++)
        {
            GameObject enemigo = Instantiate(enemigos[Random.Range(0, enemigos.Count)]);
            instancias.Add(enemigo);

            if (enemigo.GetComponent<NavMeshAgent>() != null)
            {
                enemigo.GetComponent<NavMeshAgent>().enabled = false;
            }

            enemigo.transform.position = gridEnemigos[Random.Range(0, gridEnemigos.Count)];

            if (enemigo.GetComponent<NavMeshAgent>() != null)
            {
                enemigo.GetComponent<NavMeshAgent>().enabled = true;
            }

            Vector2 vectorEnemigo = enemigo.transform.position;
            gridEnemigos.Remove(vectorEnemigo);
        }

        foreach (GameObject enemigo in instancias)
        {
            Debug.Log("Enemy position in Update: " + enemigo.transform.position);
        }

        //foreach(Vector2 vector in gridEnemigos)
        //{
        //    GameObject d = Instantiate(aa);
        //    d.transform.position = vector;
        //    instancias.Add(d);
        //}
    }
}
