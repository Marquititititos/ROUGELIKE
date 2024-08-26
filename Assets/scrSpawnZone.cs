using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrSpawnZone : MonoBehaviour
{
    public Vector2 playerSpawnPos = new Vector2(-6.5f, 0);
    public GameObject roomSpawner;
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
        roomSpawner = GameObject.Find("RoomSpawner");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.tag == "Player")
        {
            Siguiente();
        }
    }

    private void Siguiente()
    {
        Transform roomSpawnerPos = roomSpawner.transform;
        scrRoomSpawner roomSpawnerScript = roomSpawner.GetComponent<scrRoomSpawner>();

        foreach(GameObject instancia in roomSpawnerScript.instancias)
        {
            Destroy(instancia);
        }

        roomSpawnerScript.Spawnear();
        player.transform.position = playerSpawnPos;
    }
}
