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
    public Image BlackScreen2;
    public GameObject[] armas;
    private int armaSeleccionada;
    public List<GameObject> enemigosPosibles = new List<GameObject>();

    public GameObject cartelArma;
    public GameObject cartelPoder;
    public Image íconoArma;
    public Image íconoPoder;
    public List<Sprite> íconosArmas = new List<Sprite>();
    public List<Sprite> íconosPoderes = new List<Sprite>();

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
            BlackScreen2.GetComponent<Image>().color = newColor;
            yield return null;
        }

        scrRoomSpawner roomSpawnerScript = roomSpawner.GetComponent<scrRoomSpawner>();

        foreach (GameObject instancia in roomSpawnerScript.instancias)
        {
            Destroy(instancia);
        }

        yield return new WaitForSeconds(1);

        player.transform.position = new Vector2(-11.5f, -0.5f);

        foreach (GameObject balas in GameObject.FindGameObjectsWithTag("DestruirEnRespawn"))
        {
            Destroy(balas);
        }

        armaSeleccionada = Random.Range(0, armas.Length);

        Destroy(GameObject.FindGameObjectWithTag("Arma"));
        Instantiate(armas[armaSeleccionada]);
        player.GetComponent<scrPoderes>().poder = player.GetComponent<scrPoderes>().poderes[Random.Range(0, player.GetComponent<scrPoderes>().poderes.Length)];
        roomSpawnerScript.objectNumber = Random.Range(5, 15);

        cartelArma.SetActive(true);
        cartelPoder.SetActive(true);

        cartelArma.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(-133.1f, 22);

        íconoArma.GetComponent<Image>().enabled = false;

        while (BlackScreen2.GetComponent<Image>().color.a > 0)
        {
            newColor.a -= 0.01f;
            BlackScreen2.GetComponent<Image>().color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(0.15f);

        íconoArma.GetComponent<Image>().enabled = true;

        for (int i = 0; i < íconosArmas.Count; i++)
        {
            if (i != armaSeleccionada)
            {
                íconoArma.sprite = íconosArmas[i];
                yield return new WaitForSeconds(0.2f);
            }
        }

        íconoArma.sprite = íconosArmas[armaSeleccionada];

        yield return new WaitForSeconds(0.5f);

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

        switch (ronda)
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

        Color newColor2 = new Color(0, 0, 0, 1);

        while (BlackScreen.GetComponent<Image>().color.a > 0)
        {
            newColor2.a -= 0.01f;
            BlackScreen.GetComponent<Image>().color = newColor2;
            yield return null;
        }

        while (cartelArma.GetComponent<Image>().rectTransform.anchoredPosition.y < 600)
        {
            cartelArma.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(cartelArma.GetComponent<Image>().rectTransform.anchoredPosition.x, cartelArma.GetComponent<Image>().rectTransform.anchoredPosition.y + 0.25f);
            cartelPoder.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(cartelPoder.GetComponent<Image>().rectTransform.anchoredPosition.x, cartelPoder.GetComponent<Image>().rectTransform.anchoredPosition.y + 0.25f);
        }
    }

}
