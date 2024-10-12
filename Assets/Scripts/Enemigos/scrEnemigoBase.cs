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

    public float maxSpd;
    public float minSpd;
    public float spd;

    //Objetos

    public LayerMask playerLayer;
    [HideInInspector] public GameObject player;
    [HideInInspector] public scrPlayer scrplayer;
    public Animator animator;


    public virtual void Start()
    {
        player = GameObject.Find("Player");
        scrplayer = player.GetComponent<scrPlayer>();
        spd = Random.Range(minSpd, maxSpd);
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
            Destroy(gameObject);
        }
    }

    public IEnumerator Daño(float daño)
    {
        hp -= daño;

        Color color = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = color;
    }
}
