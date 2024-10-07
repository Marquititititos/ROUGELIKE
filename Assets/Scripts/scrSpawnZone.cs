using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrSpawnZone : MonoBehaviour
{
    public Vector2 playerSpawnPos = new Vector2(-6.5f, 0);
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

        player.transform.position = playerSpawnPos;
        roomSpawnerScript.Spawnear();

        while (BlackScreen.GetComponent<Image>().color.a > 0)
        {
            newColor.a -= 0.01f;
            BlackScreen.GetComponent<Image>().color = newColor;
            yield return null;
        }
    }
}
