using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class scrArmaBase : MonoBehaviour
{

    //Variables

    public float cooldown;
    private float timer;
    public bool canShoot = true;
    public float daño;

    //Objetos 

    private Vector3 mousePos;
    private Camera camara;
    public Transform posPlayer;
    public GameObject bala;
    public GameObject explosion;
    public Transform balaTransform;
    public GameObject player;

    public abstract void Disparar();

    // Start is called before the first frame update
    public void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        posPlayer = player.transform;
    }

    // Update is called once per frame
    public virtual void Update()
    {

        scrPlayer scriptPlayer = player.GetComponent<scrPlayer>();
        transform.position = new Vector2(posPlayer.position.x, posPlayer.position.y - 0.2f);
        mousePos = camara.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
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
}


