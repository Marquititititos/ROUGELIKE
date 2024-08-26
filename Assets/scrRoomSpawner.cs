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
                instancias.Add(objeto);
                objeto.transform.position = gridObjetos[Random.Range(0, gridObjetos.Count)];
                Vector2 vectorObjeto = objeto.transform.position;

                gridObjetos.Remove(vectorObjeto);
                gridEnemigos.Remove(vectorObjeto);

                //Eliminar baldosas laterales
                if (gridObjetos.Contains(new Vector2(vectorObjeto.x + 1, vectorObjeto.y))) { gridObjetos.Remove(new Vector2(vectorObjeto.x + 1, vectorObjeto.y)); }
                if (gridObjetos.Contains(new Vector2(vectorObjeto.x - 1, vectorObjeto.y))) { gridObjetos.Remove(new Vector2(vectorObjeto.x - 1, vectorObjeto.y)); }
                if (gridObjetos.Contains(new Vector2(vectorObjeto.x, vectorObjeto.y + 1))) { gridObjetos.Remove(new Vector2(vectorObjeto.x, vectorObjeto.y + 1)); }
                if (gridObjetos.Contains(new Vector2(vectorObjeto.x, vectorObjeto.y - 1))) { gridObjetos.Remove(new Vector2(vectorObjeto.x, vectorObjeto.y - 1)); }

                //Eliminar baldosas diagonales
                if (gridObjetos.Contains(new Vector2(vectorObjeto.x + 1, vectorObjeto.y + 1))) { gridObjetos.Remove(new Vector2(vectorObjeto.x + 1, vectorObjeto.y + 1)); }
                if (gridObjetos.Contains(new Vector2(vectorObjeto.x - 1, vectorObjeto.y - 1))) { gridObjetos.Remove(new Vector2(vectorObjeto.x - 1, vectorObjeto.y - 1)); }
                if (gridObjetos.Contains(new Vector2(vectorObjeto.x - 1, vectorObjeto.y + 1))) { gridObjetos.Remove(new Vector2(vectorObjeto.x - 1, vectorObjeto.y + 1)); }
                if (gridObjetos.Contains(new Vector2(vectorObjeto.x + 1, vectorObjeto.y - 1))) { gridObjetos.Remove(new Vector2(vectorObjeto.x + 1, vectorObjeto.y - 1)); }
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
