using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pathfinding;

public class scrEnemigo1 : MonoBehaviour
{

    //Variables 

    public float hp;
    public float daño;
    public float cooldown;
    public float attackRange;
    public bool canAttack = true;

    //Objetos

    public LayerMask playerLayer;
    public Transform attackPoint;
    public AIPath aipath;
    public GameObject player;
    public Animator animator;
    public GameObject ataque1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
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


        //Atacar 

        Collider2D[] playerC = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        if (playerC.Length > 0)
        {
            if (canAttack)
            {
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

        animator.SetFloat("anim", 1);

        aipath.canMove = false;
        canAttack = false;

        yield return new WaitForSeconds(1);

        animator.SetFloat("anim", 2);
        Collider2D[] playerC = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        Instantiate(ataque1, attackPoint.position, Quaternion.identity);

        if (playerC.Length > 0)
        {
            scrPlayer scrplayer = player.GetComponent<scrPlayer>();
            scrplayer.hp -= daño;
        }


        yield return new WaitForSeconds(cooldown);

        aipath.canMove = true;
        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
