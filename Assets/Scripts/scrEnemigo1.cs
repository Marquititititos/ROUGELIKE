using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class scrEnemigo1 : MonoBehaviour
{

    //Variables

    public float hp;
    public float range;
    public float daño;
    public float cooldown;
    private bool canAttack = true;

    //Objetos

    public AIPath aiPath;
    public Transform attackPoint;
    public LayerMask playerLayer;
    public Transform playerTransform;
    public AIDestinationSetter aiDestinationSetter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
