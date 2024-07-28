using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class scrEnemigo : MonoBehaviour
{

    //Variables

    public float hp;
    public float attackRange;
    public float daño;
    public float attackCooldown;
    private bool canAttack = true;

    //Objetos

    public AIPath aiPath;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public Transform playerTransform;
    public AIDestinationSetter aiDestinationSetter;
    public Transform enemyPos1;
    public Transform enemyPos2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            aiDestinationSetter.target = enemyPos1;
        } else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            aiDestinationSetter.target = enemyPos2;
        }

        //Atacar

        if (Vector2.Distance((Vector2)attackPoint.position, (Vector2)playerTransform.position) < attackRange && aiPath.desiredVelocity.x == 0 && aiPath.desiredVelocity.y == 0)
        {
            if (canAttack == true)
            {
                StartCoroutine(Atacar());
            }
        }

        //Muerte

        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private IEnumerator Atacar()
    {
        canAttack = false;

        Collider2D[] player = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach (Collider2D p in player)
        {
            scrPlayer scriptPlayer = p.GetComponent<scrPlayer>();
            scriptPlayer.hp -= daño;
        }

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }
}
