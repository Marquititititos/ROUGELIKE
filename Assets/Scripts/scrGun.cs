using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrGun : MonoBehaviour
{

    //Variables

    public float cooldown;
    private float timer;
    private bool canShoot = true;

    //Objetos 

    private Vector3 mousePos;
    private Camera camara;
    public Transform posPlayer;
    public GameObject bala;
    public Transform balaTransform;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

        scrPlayer scriptPlayer = player.GetComponent<scrPlayer>();

        transform.position = new Vector2(posPlayer.position.x, posPlayer.position.y);
        
        mousePos = camara.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

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
        Instantiate(bala, balaTransform.position, Quaternion.identity);
    }
}
