using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrPoderes : MonoBehaviour
{
    //Objetos

    public GameObject poder;
    public Image barraParaLlenar;

    //Variables

    public bool canPoder;
    public bool isLoadingBarra;
    public float cooldownPoder;
    public float cooldownPoderDuracion;
    public float timer = 0;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (canPoder)
            {
                if (poder != null)
                {
                    canPoder = false;
                    Instantiate(poder, transform.position, Quaternion.identity);
                    isLoadingBarra = false;
                }
            }
        }

        //Barra de poder

        if (isLoadingBarra)
        {
            if (barraParaLlenar.GetComponent<Image>().fillAmount < 1)
            {
                timer += Time.deltaTime;
                barraParaLlenar.GetComponent<Image>().fillAmount = Mathf.Lerp(0, 1, timer / cooldownPoder);
                barraParaLlenar.GetComponent<Animator>().SetBool("isFull", false);
            } else
            {
                barraParaLlenar.GetComponent<Image>().fillAmount = 1;
                barraParaLlenar.GetComponent<Animator>().SetBool("isFull", true);
                timer = 0;
            }
        } else
        {
            if (barraParaLlenar.GetComponent<Image>().fillAmount > 0)
            {
                timer += Time.deltaTime;
                barraParaLlenar.GetComponent<Image>().fillAmount = Mathf.Lerp(1, 0, timer / cooldownPoderDuracion);
                barraParaLlenar.GetComponent<Animator>().SetBool("isFull", false);
            } else
            {
                timer = 0;
                barraParaLlenar.GetComponent<Image>().fillAmount = 0;
            }
        }
    }

    public IEnumerator cooldown(float cooldown)
    {
        isLoadingBarra = true;
        yield return new WaitForSeconds(cooldown);
        canPoder = true;
    }
}
