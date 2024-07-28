using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrPlayer : MonoBehaviour
{
    //Variables

    public float hp = 100;
    public float spd;

    public float dashSpd;
    public float dashTime;
    public float dashCooldown;
    public bool isDashing = false;
    private bool canDash = true;
    

    //Objetos

    public Rigidbody2D rb;
    private Vector2 direccion;
    public Text txtHp;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (isDashing == false)
        {

            float hori = Input.GetAxisRaw("Horizontal");
            float verti = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(hori, verti) * spd;
            direccion = new Vector2(hori, verti).normalized;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canDash == true)
                {
                    StartCoroutine(Dash());
                }
            }
        }

        txtHp.text = hp.ToString();
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;
        rb.velocity = new Vector2(direccion.x * dashSpd, direccion.y * dashSpd);
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}