using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrSpawnZone : MonoBehaviour
{
    //Variables

    public int ronda = 1;
    private int rondasParaAumentarEnemigos = 1;
    private int contadorRondaParaAumentarEnemigos = 0;

    //Objetos
    public Vector2 playerSpawnPos = new Vector2(-11.5f, -0.5f);
    public GameObject roomSpawner;
    public GameObject player;
    public Image BlackScreen;
    public GameObject[] armas;
    public List<GameObject> enemigosPosibles = new List<GameObject>();

    private void Start()
    {
        player = GameObject.Find("Player");
        roomSpawner = GameObject.Find("RoomSpawner");
        BlackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
        roomSpawner.GetComponent<scrRoomSpawner>().enemigos.Add(enemigosPosibles[0]);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(Siguiente());
        }
    }

    private IEnumerator Siguiente()
    {

        Color newColor = Color.clear;
        while (BlackScreen.GetComponent<Image>().color.a < 1)
        {
            newColor.a += 0.01f;
            BlackScreen.GetComponent<Image>().color = newColor;
            yield return null;
        }

        scrRoomSpawner roomSpawnerScript = roomSpawner.GetComponent<scrRoomSpawner>();
 
        foreach (GameObject instancia in roomSpawnerScript.instancias)
        {
            Destroy(instancia);
        }

        yield return new WaitForSeconds(1);

        player.transform.position = new Vector2(-11.5f, -0.5f);

        foreach(GameObject balas in GameObject.FindGameObjectsWithTag("DestruirEnRespawn"))
        {
            Destroy(balas);
        }

        Destroy(GameObject.FindGameObjectWithTag("Arma"));
        Instantiate(armas[Random.Range(0, armas.Length)]);
        player.GetComponent<scrPoderes>().poder = player.GetComponent<scrPoderes>().poderes[Random.Range(0, player.GetComponent<scrPoderes>().poderes.Length)];
        roomSpawnerScript.objectNumber = Random.Range(5, 15);


        //Añadir enemigos

        ronda++;
        contadorRondaParaAumentarEnemigos++;

        if (contadorRondaParaAumentarEnemigos == rondasParaAumentarEnemigos)
        {
            if (roomSpawnerScript.enemyNumber < 16)
            {
                roomSpawnerScript.enemyNumber++;
                rondasParaAumentarEnemigos++;
                contadorRondaParaAumentarEnemigos = 0;
            }
        }

        switch(ronda)
        {
            case 2:
                roomSpawnerScript.enemigos.Add(enemigosPosibles[1]);
                roomSpawnerScript.enemigos.Add(enemigosPosibles[2]);
                break;

            case 4:
                roomSpawnerScript.enemigos.Add(enemigosPosibles[3]);
                roomSpawnerScript.enemigos.Add(enemigosPosibles[4]);
                roomSpawnerScript.enemigos.Add(enemigosPosibles[5]);
                break;

            case 6:
                roomSpawnerScript.enemigos.Add(enemigosPosibles[6]);
                roomSpawnerScript.enemigos.Add(enemigosPosibles[7]);
                break;

            case 8:
                roomSpawnerScript.enemigos.Add(enemigosPosibles[3]);
                roomSpawnerScript.enemigos.Add(enemigosPosibles[4]);
                break;

            case 10:
                roomSpawnerScript.enemigos.Add(enemigosPosibles[5]);
                roomSpawnerScript.enemigos.Add(enemigosPosibles[6]);
                break;

        }

        roomSpawnerScript.Spawnear();

        while (BlackScreen.GetComponent<Image>().color.a > 0)
        {
            newColor.a -= 0.01f;
            BlackScreen.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }

}
