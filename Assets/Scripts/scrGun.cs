using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGun : MonoBehaviour
{

    //Variables

    public float cooldown;
    private float timer;
    private bool canShoot = true;
    public float da�o;

    //Objetos 

    private Vector3 mousePos;
    private Camera camara;
    public Transform posPlayer;
    public GameObject bala;
    public GameObject explosion;
    public Transform balaTransform;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        posPlayer = player.transform;
    }

    // Update is called once per frame
    void Update()
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

        if (Input.GetMouseButton(0) && canShoot == true && scriptPlayer.isDashing == false)
        {
            Disparar();
        }

        if (canShoot == false)
        {
            if (timer < cooldown)
            {
                timer += Time.deltaTime;
            }
            else
            {
                canShoot = true;
                timer = 0;
            }
        }
    }

    void Disparar()
    {
        canShoot = false;
        GameObject balaDisparada = Instantiate(bala, balaTransform.position, Quaternion.identity);
        balaDisparada.GetComponent<scrBala>().da�o = da�o;
        GameObject explo = Instantiate(explosion, balaTransform.position, Quaternion.identity);
        explo.transform.SetParent(this.transform);
    }
}
