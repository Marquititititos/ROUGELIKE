using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrArmaLáser : MonoBehaviour
{
    //Variables

    public float cooldown;
    public bool canShoot = true;
    public float daño;

    //Objetos 

    private Vector3 mousePos;
    private Camera camara;
    public Transform posPlayer;
    public GameObject bala;
    public GameObject explosion;
    public GameObject player;
    public Transform balaTransform;
    public Animator anim;
    public LayerMask enemigosLayer;

    public void Start()
    {
        camara = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.Find("Player");
        posPlayer = player.transform;
    }
    private void Update()
    {
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

        if (Input.GetMouseButtonDown(0))
        {
            if (canShoot)
            {
                StartCoroutine(Disparo());
            }
        }
    }

    private IEnumerator Disparo()
    {
        canShoot = false;
        mousePos = camara.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        bala.SetActive(true);
        GameObject explo = Instantiate(explosion, balaTransform.position, Quaternion.Euler(0, 0, rotZ));
        explo.transform.SetParent(gameObject.transform);

        RaycastHit2D[] hits = Physics2D.RaycastAll(balaTransform.position, (mousePos - balaTransform.position).normalized, bala.transform.localScale.x, enemigosLayer);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null)
            {
                Debug.Log(hit.collider.name);
                hit.collider.GetComponent<scrEnemigoBase>().StartCoroutine(hit.collider.GetComponent<scrEnemigoBase>().Daño(daño));
            }
        }

        yield return new WaitForSeconds(0.2f);
        bala.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }
}
