using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class scrEnemigoBase : MonoBehaviour
{

    /// VARIABLES BÁSICAS, GIRAR HACIA EL JUGADOR, SUFRIR DAÑO Y MORIR


    //Variables 

    public float hp;
    public float daño;
    public float cooldown;
    public float attackRange;
    public bool canAttack = true;
    public bool isFrozen = false;

    public float maxSpd;
    public float minSpd;
    public float spd;

    public float minCooldown;
    public float maxCooldown;

    //Objetos

    public LayerMask playerLayer;
    [HideInInspector] public GameObject player;
    [HideInInspector] public scrPlayer scrplayer;
    public Animator animator;
    private Color colorBase;
    public GameObject explosionMuerte;

    [HideInInspector] public Material materialBase;

    public virtual void Start()
    {
        spd = Random.Range(minSpd, maxSpd);
        colorBase = GetComponent<SpriteRenderer>().color;
        player = GameObject.Find("Player");
        scrplayer = player.GetComponent<scrPlayer>();
        materialBase = new Material(Shader.Find("Sprites/Default"));
        GetComponent<SpriteRenderer>().material = materialBase;
    }

    public virtual void Update()
    {
       

        //Girar

        if (canAttack)
        {
            if (player.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(-1, 1);
            }
            else
            {
                transform.localScale = new Vector2(1, 1);
            }
        }

        //Morir

        if (hp <= 0)
        {
            if (GetComponent<scrBombita>() == null || GetComponent<scrBombita>() != null && GetComponent<scrBombita>().isCuchillo == true)
            {
                Instantiate(explosionMuerte, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }

    public IEnumerator Daño(float daño, bool cuchillo = false)
    {
        if (cuchillo == true)
        {
            if (GetComponent<scrBombita>() != null)
            {
                GetComponent<scrBombita>().isCuchillo = true;
            }
        }

        hp -= daño;
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<SpriteRenderer>().material = new Material(Shader.Find("GUI/Text Shader"));
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = colorBase;
        GetComponent<SpriteRenderer>().material = materialBase;

        if (cuchillo == true)
        {
            if (GetComponent<scrBombita>() != null)
            {
                GetComponent<scrBombita>().isCuchillo = false;
            }
        }
    }
}
