using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrEnemyPos : MonoBehaviour
{

    public Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "enemyPos2")
        {
            transform.position = new Vector2(playerTransform.position.x + 1, playerTransform.position.y);
        }

        if (this.gameObject.tag == "enemyPos1")
        {
            transform.position = new Vector2(playerTransform.position.x - 1, playerTransform.position.y);
        }
    }
}

