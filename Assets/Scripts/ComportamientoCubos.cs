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

    private AudioSource audioSource;

    void Start()
    {
        generadorObstaculos = FindObjectOfType<GeneradorObstaculos>();
    }

    // Update is called once per frame
    void Update()
    {


        audioSource = GetComponent<AudioSource>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Pelota")
        {

            LeanTween.alpha(gameObject, 0.2f, 0.15f).setLoopPingPong(1).setOnComplete(() => { });


            vidasCubo--;
            if (vidasCubo <= 0)
            {
                audioSource.Play();
                PuntosManager.Instancia.SumarPuntos(sumaPuntos);
                PuntosManager.Instancia.RestarCubos();
                Destroy(gameObject);
            }

            
        }
    }

}
