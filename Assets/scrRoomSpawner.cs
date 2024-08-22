using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrRoomSpawner : MonoBehaviour
{
    //Variables

    private float gridWidth = 14;
    private float gridHeight = 6;
    public float objectNumber;

    //Objetos

    public GameObject bloque;
    private List<Vector2> gridPositions = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                gridPositions.Add(new Vector2(x, y));
            }
        }

        for (int i = 0; i < objectNumber; i++)
        {
            GameObject objeto = Instantiate(bloque);
            objeto.transform.position = gridPositions[Random.Range(0, gridPositions.Count)];
            Vector2 vectorObjeto = objeto.transform.position;


        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
