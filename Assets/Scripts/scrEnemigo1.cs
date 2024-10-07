using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class scrEnemigo1 : MonoBehaviour
{

    //Variables 

    public float hp;
    public float daño;
    public float cooldown;
    public float attackRange;
    public bool canAttack = true;

    public float maxSpd;
    public float minSpd;
    private float spd;

    //Objetos

    public LayerMask playerLayer;
    public Transform attackPoint;
    public GameObject player;
    public Animator animator;
    public GameObject ataque1;
    private NavMeshAgent agent;

    // Start is called before the first frame update

    void Start()
    {
        player = GameObject.Find("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        spd = Random.Range(minSpd, maxSpd);
        agent.speed = spd;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.transform.position);

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


        //Atacar 

        Collider2D[] playerC = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        if (playerC.Length > 0)
        {
            if (canAttack)
            {
                animator.SetFloat("anim", 1);
                StartCoroutine(Atacar());
            }
        }

        //Morir

        if (hp <= 0)
        {
            Destroy(gameObject);
        }


        if (canAttack)
        {
            animator.SetFloat("anim", 0);
        }
    }

    private IEnumerator Atacar()
    {

        agent.speed = 0;
        canAttack = false;

        yield return new WaitForSeconds(2);

        animator.SetFloat("anim", 2);
        Collider2D[] playerC = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        Instantiate(ataque1, attackPoint.position, Quaternion.identity);

        if (playerC.Length > 0)
        {
            scrPlayer scrplayer = player.GetComponent<scrPlayer>();
            if (scrplayer.invincible == false)
            {
                scrplayer.hp -= daño;
            }
        }


        yield return new WaitForSeconds(cooldown);

        agent.speed = spd;
        animator.SetFloat("anim", 0);
        canAttack = true;
    }

    public IEnumerator Daño(float daño)
    {
        hp -= daño;
        GetComponent<SpriteRenderer>().color = Color.gray;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
