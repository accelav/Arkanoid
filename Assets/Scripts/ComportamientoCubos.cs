using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoCubos : MonoBehaviour
{
    public int vidasCubo = 2;
    public int puntos;
    public int sumaPuntos = 500;
    public bool haGolpeado =  false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        puntos += sumaPuntos;
        if (collision.gameObject.tag == "Pelota")
        {
            haGolpeado = true;
            vidasCubo--;
            if (vidasCubo <= 0)
            {
                
                gameObject.SetActive(false);
            }

            
        }
    }

}
