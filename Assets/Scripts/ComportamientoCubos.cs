using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class ComportamientoCubos : MonoBehaviour
{

    public int vidasCubo = 2;

    public int sumaPuntos = 500;
    public bool haGolpeado =  false;
    private GeneradorObstaculos generadorObstaculos;
    private int vecesRoto = 1;

    public GameObject[] powerUps;
    GameObject elegirPowerUp;

    public Vector3 posicionObjeto;

    [SerializeField]
    private AudioClip romperCubo;

    void Start()
    {
        generadorObstaculos = FindObjectOfType<GeneradorObstaculos>();
    }

    // Update is called once per frame
    void Update()
    {
        posicionObjeto = gameObject.transform.position;

    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.tag == "Pelota")
        {
            posicionObjeto = gameObject.transform.position;

            elegirPowerUp = powerUps[Random.Range(0, powerUps.Length)];

            LeanTween.alpha(gameObject, 0.2f, 0.15f).setLoopPingPong(1).setOnComplete(() => { });

            vidasCubo--;
            
            if (vidasCubo <= 0)
            {
                LeanTween.alpha(gameObject, 0f, 0.15f).setLoopPingPong(1).setOnComplete(() =>
                {


                    ControladorDeSonidos.instance.EjecutarSonido(romperCubo);

                    PuntosManager.Instancia.SumarPuntos(sumaPuntos);
                    PuntosManager.Instancia.RestarCubos();

                    PuntosManager.Instancia.ContarRotura(vecesRoto);

                    if (PuntosManager.Instancia.vecesRoto == 10)
                    {
                        Instantiate(elegirPowerUp, posicionObjeto, Quaternion.Euler(-90, 0, 0));

                        PuntosManager.Instancia.vecesRoto = 0;
                    }
                    
                    

                    Destroy(gameObject);

                    
                });
               
            }
            else
            {
                LeanTween.alpha(gameObject, 0f, 0.15f).setLoopPingPong(1).setOnComplete(() => { });
            }

            
        }
    }

}
