using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public GameObject[] poderes;
    private int armaSeleccionada;
    private int poderSeleccionado;
    public List<GameObject> enemigosPosibles = new List<GameObject>();

    public GameObject cartelArma;
    public GameObject cartelPoder;
    public Image �conoArma;
    public Image �conoPoder;
    public Image �conoPoderUI;
    public TMP_Text t�tuloArma;
    public TMP_Text descArma;
    public TMP_Text t�tuloPoder;
    public TMP_Text descPoder;
    public List<Sprite> �conosArmas = new List<Sprite>();
    public List<Sprite> �conosPoderes = new List<Sprite>();

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

        yield return new WaitForSeconds(0.5f);

        foreach (GameObject balas in GameObject.FindGameObjectsWithTag("DestruirEnRespawn"))
        {
            Destroy(balas);
        }

        armaSeleccionada = Random.Range(0, armas.Length);
        poderSeleccionado = Random.Range(0, poderes.Length);

        Destroy(GameObject.FindGameObjectWithTag("Arma"));
        Instantiate(armas[armaSeleccionada]);
        player.GetComponent<scrPoderes>().poder = poderes[poderSeleccionado];
        roomSpawnerScript.objectNumber = Random.Range(5, 15);

        cartelArma.SetActive(true);
        cartelPoder.SetActive(true);

        t�tuloArma.text = "";
        t�tuloPoder.text = "";
        descArma.text = "";
        descPoder.text = "";

        cartelArma.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(-133.1f, 22);
        cartelPoder.GetComponent<Image>().rectTransform.anchoredPosition = new Vector2(129, 22);

        �conoArma.GetComponent<Image>().enabled = false;
        �conoPoder.GetComponent<Image>().enabled = false;
        �conoPoderUI.GetComponent<Image>().enabled = false;

        while (BlackScreen2.GetComponent<Image>().color.a > 0)
        {
            newColor.a -= 0.01f;
            BlackScreen2.GetComponent<Image>().color = newColor;
            yield return null;
        }

        yield return new WaitForSeconds(0.15f);

        �conoArma.GetComponent<Image>().enabled = true;
        �conoPoder.GetComponent<Image>().enabled = true;

        StartCoroutine(RuletaArma());
        StartCoroutine(RuletaPoder());

        yield return new WaitForSeconds(2);

        //A�adir enemigos

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

        player.transform.position = new Vector2(-11.5f, -0.5f);
        player.GetComponent<scrPlayer>().spd = 6;

        Color newColor2 = new Color(0, 0, 0, 1);

        while (BlackScreen.GetComponent<Image>().color.a > 0)
        {
            newColor2.a -= 0.01f;
            BlackScreen.GetComponent<Image>().color = newColor2;
            yield return null;
        }

        yield return new WaitForSeconds(1);

        while (cartelArma.GetComponent<Image>().rectTransform.anchoredPosition.y < 600)
        {
            cartelArma.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1000);
            cartelPoder.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1000);
            yield return null;
        }

        cartelArma.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        cartelPoder.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    private IEnumerator RuletaArma()
    {

        for (int i = 0; i < �conosArmas.Count; i++)
        {
            if (i != armaSeleccionada)
            {
                �conoArma.sprite = �conosArmas[i];
                yield return new WaitForSeconds(0.2f);
            }
        }

        t�tuloArma.text = armas[armaSeleccionada].GetComponent<scrDatos>().t�tulo;
        descArma.text = armas[armaSeleccionada].GetComponent<scrDatos>().desc;
        �conoArma.sprite = �conosArmas[armaSeleccionada];
    }

    private IEnumerator RuletaPoder()
    {

        for (int i = 0; i < �conosPoderes.Count; i++)
        {
            if (i != poderSeleccionado)
            {
                �conoPoder.sprite = �conosPoderes[i];
                yield return new WaitForSeconds(0.28f);
            }
        }

        t�tuloPoder.text = poderes[poderSeleccionado].GetComponent<scrPoderBase>().t�tulo;
        descPoder.text = poderes[poderSeleccionado].GetComponent<scrPoderBase>().desc;
        �conoPoder.sprite = �conosPoderes[poderSeleccionado];
        �conoPoderUI.GetComponent<Image>().enabled = true;
        �conoPoderUI.sprite = �conosPoderes[poderSeleccionado];
    }
}
