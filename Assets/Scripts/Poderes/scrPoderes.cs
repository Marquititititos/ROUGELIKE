using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrPoderes : MonoBehaviour
{
    //Objetos

    public Text cooldownPoderText;
    public GameObject[] poderes;
    public GameObject poder;

    //Variables

    public bool canPoder;

    // Start is called before the first frame update
    void Start()
    {
        canPoder = true;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownPoderText.text = canPoder.ToString();
        if (Input.GetMouseButtonDown(1)) {
            if (canPoder)
            {
                if (poder != null)
                {
                    canPoder = false;
                    Instantiate(poder, transform.position, Quaternion.identity);
                }
            }
        }
    }

    public IEnumerator cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        canPoder = true;
    }
}
