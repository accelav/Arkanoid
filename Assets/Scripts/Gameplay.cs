using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public MovimientoPelota MovimientoPelota;
    public GeneradorObstaculos GeneradorObstaculos;
    public MoviemientoPlataforma MoviemientoPlataforma;
    //public ComportamientoCubos ComportamientoCubos;
    //public PuntosManager puntosManager;
    [SerializeField]
    GameObject botonesInicio;
    [SerializeField]
    GameObject sombreado;



    public GameObject esfera;
    public GameObject plataforma;
    public bool estaIniciada = false;
    public float velocidad = 0.5f;
    Vector3 posicionInicialPlataforma = Vector3.zero;
    Vector3 posicionInicialEsfera = new Vector3(0, 0.12f, 0);

    [SerializeField]
    public TextMeshProUGUI textoVida;
    public int vida;
    public int nuevaVida = 2;
    [SerializeField]
    public GameObject imagenGameOver;

    [SerializeField]
    TextMeshProUGUI textoTiempo;
    public float tiempo = 0f;
    public bool estaContando = false;

    [SerializeField]
    public TextMeshProUGUI textoPuntos;
    public TextMeshProUGUI puntosFinales;
    public TextMeshProUGUI puntosRecord;
    public TextMeshProUGUI puntosRecord2;
    public int puntos;
    public int puntosTotales;
    public bool haTocado = false;
    [SerializeField]
    public GameObject canvasPuntosFinales;
    int ultimoRecord;
    int puntosFinalesInt;

    [SerializeField]
    public GameObject textos;


    public string tagObjetos = "Cube"; // El tag de los objetos que deseas comprobar
    private GameObject[] Cubos;
    public bool todosInactivos = false;
    int unoMenos;
    public int cuantosCubosQuedan;
    public int numeroObstaculos;

    public GameObject imagenHasGanado;

    [SerializeField]
    GameObject imagenInicio;

    float velocidadPelota;
    float velocidadPlataforma;

    bool reiniciandoPartida = false;

    [SerializeField]
    private AudioClip pulsacionBoton;

    [SerializeField]
    GameObject opcionesCanvas;
    bool opciones = false;
    bool atras = false;

    void Start()
    {
        estaIniciada = false;
        Cubos = GameObject.FindGameObjectsWithTag(tagObjetos);
        textos.SetActive(false);
        velocidadPelota = MovimientoPelota.velocidad;
        velocidadPlataforma = MoviemientoPlataforma.velocidad;


    }


    void Update()
    {
        numeroObstaculos = GeneradorObstaculos.n;
        cuantosCubosQuedan = numeroObstaculos + PuntosManager.Instancia.restarCubos;

        


        textoTiempo.text = tiempo.ToString("00");
        textoVida.text = vida.ToString();
        textoPuntos.text =  PuntosManager.Instancia.ObtenerPuntos().ToString();
        puntosFinales.text = PuntosManager.Instancia.ObtenerPuntos().ToString();
        puntosRecord.text = PuntosManager.Instancia.ObtenerRecord().ToString();
        puntosRecord2.text = PuntosManager.Instancia.ObtenerRecord().ToString();

        if (estaIniciada)
        {
            

            LeanTween.moveLocalY(botonesInicio, -700f, velocidad).setEase(LeanTweenType.easeOutSine);

            sombreado.SetActive(true);

            imagenInicio.SetActive(false);

            textos.SetActive(true);
            
            imagenHasGanado.SetActive(false);

            estaIniciada = false;

        }

        if (reiniciandoPartida)
        {
            imagenGameOver.SetActive(false);
            LeanTween.moveLocalY(botonesInicio, 0f, velocidad).setEase(LeanTweenType.easeOutSine);

            sombreado.SetActive(false);

            LeanTween.alpha(esfera, 0f, 0.4f);
            LeanTween.alpha(plataforma, 0f, 0.4f);

            imagenInicio.SetActive(true);

            canvasPuntosFinales.SetActive(false);
            

            imagenHasGanado.SetActive(false);

            reiniciandoPartida = false;
        }

        if (opciones)
        {
            LeanTween.moveLocalY(botonesInicio, -700f, velocidad).setEase(LeanTweenType.easeOutSine);
            imagenInicio.SetActive(false);
            opcionesCanvas.SetActive(true);
            opciones = false;
        }

        if (atras)
        {
            LeanTween.moveLocalY(botonesInicio, 0f, velocidad).setEase(LeanTweenType.easeOutSine);
            imagenInicio.SetActive(true);
            opcionesCanvas.SetActive(false);
            atras = false;
        }

        if (estaContando)
        {
            tiempo += Time.deltaTime;

            if (numeroObstaculos + PuntosManager.Instancia.restarCubos == 0)
            {
                numeroObstaculos = 0;
                PuntosManager.Instancia.restarCubos = 0;
                GeneradorObstaculos.DestruirObstaculos();
                if (GeneradorObstaculos.obstáculosGenerados == false)
                {
                    GeneradorObstaculos.ColocarObstaculos();
                }
                //MovimientoPelota.velocidad = MovimientoPelota.velocidad * 1.2f;
                //MoviemientoPlataforma.velocidad = MoviemientoPlataforma.velocidad * 1.2f;

                //imagenHasGanado.SetActive(true);
                //estaContando = false;
                //MovimientoPelota.moviendo = false;
               // textos.SetActive(false);
                //canvasPuntosFinales.SetActive(true);
            }

        }
        if (vida <= 0)
        {
            GeneradorObstaculos.DestruirObstaculos();
            sombreado.SetActive(false);
        }

    }

    public void IniciarPartida()
    {
        estaIniciada = true;
        vida = nuevaVida;
        PuntosManager.Instancia.ReiniciarPuntos();
        GeneradorObstaculos.ColocarObstaculos();
        cuantosCubosQuedan = 0;

        ControladorDeSonidos.instance.EjecutarSonido(pulsacionBoton);



    }

    public void ReiniciarPartida()
    {
        reiniciandoPartida = true;
        textos.SetActive(false);
        puntos = 0;
        tiempo = 0;
        numeroObstaculos = 0;
        PuntosManager.Instancia.restarCubos = 0;
        GeneradorObstaculos.DestruirObstaculos();


        ControladorDeSonidos.instance.EjecutarSonido(pulsacionBoton);

    }

    public void Opciones()
    {
        opciones = true;
    }

    public void Atras()
    {
        atras = true;
    }

}
