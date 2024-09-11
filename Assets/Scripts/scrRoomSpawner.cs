using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrRoomSpawner : MonoBehaviour
{
    //Variables

    private float gridWidth = 14;
    private float gridHeight = 6;
    public float objectNumber;
    public float enemyNumber;

    //Objetos

    public GameObject bloque;
    public GameObject enemy;
    public GameObject enviroment;
    private List<Vector2> gridObjetos = new List<Vector2>();
    private List<Vector2> gridEnemigos = new List<Vector2>();
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

        gridObjetos.Clear();
        gridEnemigos.Clear();

        //Crear grid objetos
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                gridObjetos.Add(new Vector2(x - 3.5f, y - 2.5f));
            }
        }

        //Crear grid enemigos
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                gridEnemigos.Add(new Vector2(x - 3.5f, y - 2.5f));
            }
        }

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

                Vector2 minBound = vectorObjeto - new Vector2(2, 2);
                Vector2 maxBound = vectorObjeto + new Vector2(2, 2);

                gridObjetos.RemoveAll(v => v.x >= minBound.x && v.x <= maxBound.x && v.y >= minBound.y && v.y <= maxBound.y);
            }
        }

        //Spawnear enemigos
        for (int i = 0; i < enemyNumber; i++)
        {
            GameObject enemigo = Instantiate(enemy);
            instancias.Add(enemigo);
            enemigo.transform.position = gridEnemigos[Random.Range(0, gridEnemigos.Count)];
            Vector2 vectorEnemigo = enemigo.transform.position;

            gridEnemigos.Remove(vectorEnemigo);
        }
    }
}
