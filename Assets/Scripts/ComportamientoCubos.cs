using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ComportamientoCubos : MonoBehaviour
{

    public int vidasCubo = 2;

    public int sumaPuntos = 500;
    public bool haGolpeado =  false;
    private GeneradorObstaculos generadorObstaculos;

    void Start()
    {
        generadorObstaculos = FindObjectOfType<GeneradorObstaculos>();
    }

    // Update is called once per frame
    void Update()
    {


        

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Pelota")
        {
            
            vidasCubo--;
            if (vidasCubo <= 0)
            {
                PuntosManager.Instancia.SumarPuntos(sumaPuntos);
                PuntosManager.Instancia.RestarCubos();
                Destroy(gameObject);
            }

            
        }
    }

}
