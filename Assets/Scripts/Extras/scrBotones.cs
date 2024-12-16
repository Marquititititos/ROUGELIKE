using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class scrBotones : MonoBehaviour
{
    public bool isPaused;
    public int rondaSave;

    public static scrBotones instance;
    public Image blackScreen;

    void Awake()
    {
        // Ensure this GameObject persists between scenes
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(blackScreen.gameObject);

        if (SceneManager.GetActiveScene().name == "Inicio")
        {
            Highscore();
        }
    }

    public void CambioDeEscena(string escena)
    {
        StartCoroutine(CambiarEscena(escena));
    }

    public IEnumerator CambiarEscena(string escena)
    {
        Time.timeScale = 1;

        if (escena == "Tutorial" || escena == "Game")
        {
            Cursor.visible = false;
        } else
        {
            Cursor.visible = true;
            GameObject.Find("Puntero").GetComponent<SpriteRenderer>().enabled = false;
        }

        blackScreen = GameObject.Find("Transicion").GetComponent<Image>();

        while(blackScreen.color.a < 1)
        {
            blackScreen.color = new Color(0, 0, 0, blackScreen.color.a + Time.deltaTime);
            yield return null;
        }

        SceneManager.LoadScene(escena);

        yield return null;

        if (escena == "Inicio")
        {
            Highscore();
        }

        blackScreen = GameObject.Find("Transicion").GetComponent<Image>();
        blackScreen.color = new Color(0, 0, 0, 1);

        yield return new WaitForSeconds(1);

        while (blackScreen.color.a > 0)
        {
            blackScreen.color = new Color(0, 0, 0, blackScreen.color.a - Time.deltaTime);
            yield return null;
        }

        isPaused = false;
    }

    public void Pausa()
    {
        if (GameObject.Find("spawnZone") == null || GameObject.Find("spawnZone").GetComponent<scrSpawnZone>().isTransitioning == false)
        {
            GameObject puntero = GameObject.Find("Puntero");
            if (!isPaused)
            {
                puntero.GetComponent<SpriteRenderer>().enabled = false;
                Cursor.visible = true;
                Time.timeScale = 0;
                isPaused = true;
                GameObject.Find("Player").GetComponent<scrPlayer>().pauseMenu.SetActive(true);
            }
            else
            {
                puntero.GetComponent<SpriteRenderer>().enabled = true;
                Cursor.visible = false;
                isPaused = false;
                Time.timeScale = 1;
                GameObject.Find("Player").GetComponent<scrPlayer>().pauseMenu.SetActive(false);
            }
        }
    }

    public void Highscore()
    {
        if (rondaSave > PlayerPrefs.GetInt("Highscore", 1))
        {
            PlayerPrefs.SetInt("Highscore", rondaSave);
        }

        GameObject.Find("txtHighscore").GetComponent<TMP_Text>().text = "HIGHSCORE: " + PlayerPrefs.GetInt("Highscore", 1);
    }
}
