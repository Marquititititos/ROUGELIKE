using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrPlayer : MonoBehaviour
{
    //Variables

    public float hp = 50;
    public float spd;

    public float dashSpd;
    public float dashTime;
    public float dashCooldown;
    public bool isDashing = false;
    private bool canDash = true;
    private bool isRight = true;

    public bool invincible = false;
    public bool isBurbuja;
    

    //Objetos

    public Rigidbody2D rb;
    private Vector2 direccion;
    public Animator animator;
    public TrailRenderer tr;
    public GameObject[] vidas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isBurbuja)
        {
            invincible = true;
        }

        if (isDashing == false)
        {

            float hori = Input.GetAxisRaw("Horizontal");
            float verti = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(hori, verti) * spd;
            direccion = new Vector2(hori, verti).normalized;

            if (rb.velocity.x  != 0 || rb.velocity.y != 0)
            {
                animator.SetFloat("Speed", 1);
            } else
            {
                animator.SetFloat("Speed", 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canDash == true)
                {
                    StartCoroutine(Dash());
                }
            }
        }

        Flip();
    }

    private IEnumerator Dash()
    {
        tr.emitting = true;
        invincible = true;
        isDashing = true;
        canDash = false;
        rb.velocity = new Vector2(direccion.x * dashSpd, direccion.y * dashSpd);

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        tr.emitting = false;

        yield return new WaitForSeconds(dashCooldown);

        canDash = true;
        invincible = false;
    }

    private void Flip()
    {
        if (isRight && rb.velocity.x < 0 || !isRight && rb.velocity.x > 0)
        {
            isRight = !isRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    public IEnumerator Golpe(float daño)
    {
        if (invincible == false)
        {
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.3f);
            hp -= daño;
            invincible = true;
            vidas[(int)hp / 10].GetComponent<Animator>().SetTrigger("golpe");
            yield return new WaitForSeconds(2);
            invincible = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
