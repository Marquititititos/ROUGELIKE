using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrPoderes : MonoBehaviour
{
    //Objetos

    public GameObject[] poderes;
    public GameObject poder;

    //Variables

    public bool canPoder;

    // Start is called before the first frame update
    void Start()
    {
        poder = poderes[Random.Range(0, poderes.Length)];
        canPoder = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            if (canPoder)
            {
                StartCoroutine(Poder());
            }
        }
    }

    private IEnumerator Poder()
    {
        canPoder = false;
        Instantiate(poder, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(poder.GetComponent<scrPoderBase>().cooldown);
        canPoder = true;
    }
}
