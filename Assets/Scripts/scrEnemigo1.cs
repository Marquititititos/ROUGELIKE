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

    }

    private IEnumerator Atacar()
    {
        aipath.canMove = false;
        canAttack = false;

        yield return new WaitForSeconds(1);

        Collider2D[] playerC = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        if (playerC.Length > 0)
        {
            scrPlayer scrplayer = player.GetComponent<scrPlayer>();
            scrplayer.hp -= daño;
        }

        aipath.canMove = true;

        yield return new WaitForSeconds(cooldown);

        canAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(attackPoint.position, attackRange);
    }
}
