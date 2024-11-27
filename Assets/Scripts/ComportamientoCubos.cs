using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoCubos : MonoBehaviour
{
    public int vidasCubo = 2;

    public bool haGolpeado =  false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if (haGolpeado)
        {
            vidasCubo--;

            if (vidasCubo == 0)
            {
                gameObject.SetActive(false);
            }
        }*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Pelota")
        {
            vidasCubo--;
            if (vidasCubo <= 0)
            {
                
                gameObject.SetActive(false);
            }

            
        }
    }

}
