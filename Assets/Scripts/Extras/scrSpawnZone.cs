using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrSpawnZone : MonoBehaviour
{
    //Variables

    public GameObject[] armas;

    //Objetos
    public Vector2 playerSpawnPos = new Vector2(-11.5f, -0.5f);
    public GameObject roomSpawner;
    public GameObject player;
    public Image BlackScreen;

    private void Start()
    {
        player = GameObject.Find("Player");
        roomSpawner = GameObject.Find("RoomSpawner");
        BlackScreen = GameObject.Find("BlackScreen").GetComponent<Image>();
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

        roomSpawnerScript.Spawnear();

        while (BlackScreen.GetComponent<Image>().color.a > 0)
        {
            newColor.a -= 0.01f;
            BlackScreen.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }
}
