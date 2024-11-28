using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public MovimientoPelota MovimientoPelota;
    //public PuntosManager puntosManager;
    [SerializeField]
    GameObject botonIniciar;
    [SerializeField]
    GameObject botonOpciones;
    [SerializeField]
    GameObject botonSalir;
    
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
    public bool todosDesactivados = false;

    public GameObject imagenHasGanado;

    bool reiniciandoPartida = false;
    void Start()
    {
        estaIniciada = false;
        Cubos = GameObject.FindGameObjectsWithTag(tagObjetos);
        textos.SetActive(false);
        ComportamientoCubos comportamientoCubos = FindObjectOfType<ComportamientoCubos>();
        puntos = comportamientoCubos.puntos;




    }

    // Update is called once per frame
    void Update()
    {

        textoTiempo.text = tiempo.ToString("00");
        textoVida.text = vida.ToString();
        textoPuntos.text =  PuntosManager.Instancia.ObtenerPuntos().ToString();
        puntosFinales.text = "Puntos Finales: " + PuntosManager.Instancia.ObtenerPuntos().ToString();
        puntosRecord.text = "Record Anterior: " + PuntosManager.Instancia.ObtenerRecord();

        if (estaIniciada)
        {
            LeanTween.moveLocalX(botonIniciar, 1000f, velocidad).setEase(LeanTweenType.easeOutSine);
            LeanTween.moveLocalX(botonOpciones, 1000f, velocidad).setEase(LeanTweenType.easeOutSine);
            LeanTween.moveLocalX(botonSalir, 1000f, velocidad).setEase(LeanTweenType.easeOutSine);

            textos.SetActive(true);
            

            todosDesactivados = false;
            imagenHasGanado.SetActive(false);

            estaIniciada = false;

        }

        if (reiniciandoPartida)
        {
            imagenGameOver.SetActive(false);
            LeanTween.moveLocalX(botonIniciar, 0, velocidad).setEase(LeanTweenType.easeOutSine);
            LeanTween.moveLocalX(botonOpciones, 0, velocidad).setEase(LeanTweenType.easeOutSine);
            LeanTween.moveLocalX(botonSalir, 0f, velocidad).setEase(LeanTweenType.easeOutSine);

            LeanTween.alpha(esfera, 0f, 0.4f);
            LeanTween.alpha(plataforma, 0f, 0.4f);

            canvasPuntosFinales.SetActive(false);


            todosDesactivados = false;
            imagenHasGanado.SetActive(false);

            reiniciandoPartida = false;
        }

        if (estaContando)
        {
            tiempo += Time.deltaTime;
            
        }

        if (!todosInactivos && MovimientoPelota.moviendo)
        {
            todosDesactivados = true;

            // Revisar cada objeto en el arreglo
            foreach (GameObject objeto in Cubos)
            {
                // Si algún objeto está activo, seguimos esperando
                if (objeto.activeSelf)
                {
                    todosDesactivados = false;
                    break;
                }
            }

            if (todosDesactivados)
            {
                todosInactivos = true;
                imagenHasGanado.SetActive(true);
                estaContando = false;
                MovimientoPelota.moviendo = false;
                textos.SetActive(false);
                canvasPuntosFinales.SetActive(true);
            }
        }


    }

    public void IniciarPartida()
    {
        estaIniciada = true;
        vida = nuevaVida;
        PuntosManager.Instancia.ReiniciarPuntos();

    }

    public void ReiniciarPartida()
    {
        reiniciandoPartida = true;
        textos.SetActive(false);
        puntos = 0;
        tiempo = 0;
        

    }


}
