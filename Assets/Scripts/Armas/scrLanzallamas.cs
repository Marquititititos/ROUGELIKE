using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrLanzallamas : MonoBehaviour
{

    //Variables

    public float cooldown;
    public bool canShoot = true;
    public float daño;
    private bool isShooting;

    //Objetos 

    private Vector3 mousePos;
    private Camera camara;
    public Transform posPlayer;
    public GameObject bala;
    public GameObject explosion;
    public Transform balaTransform;
    public GameObject player;
    public Animator anim;

    public void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        posPlayer = player.transform;
    }
    private void Update()
    {
        scrPlayer scriptPlayer = player.GetComponent<scrPlayer>();
        transform.position = new Vector2(posPlayer.position.x, posPlayer.position.y - 0.2f);
        mousePos = camara.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (rotZ > 180)
        {
            rotZ -= 360;
        }

        if (rotZ > -90 && rotZ < 90)
        {
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            transform.localScale = new Vector2(1, -1);
        }

        if (!isShooting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isShooting = true;
                anim.SetTrigger("Start");
                bala.GetComponent<scrFuego>().cooldown = 0;
            }
        }

            if (Input.GetMouseButtonUp(0))
            {
                StartCoroutine(End());
            }

        if (isShooting)
        {
            bala.SetActive(true);
        } else
        {
            bala.SetActive(false);
        }
    }

    private IEnumerator End()
    {
        anim.SetTrigger("End");
        yield return new WaitForSeconds(0.15f);
        isShooting = false;
    }
}
