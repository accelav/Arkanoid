using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    public MovimientoPelota MovimientoPelota;

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
    public int puntos;

    [SerializeField]
    GameObject textos;


    public string tagObjetos = "Cube"; // El tag de los objetos que deseas comprobar
    private GameObject[] Cubos;
    public bool todosInactivos = false;
    public bool todosDesactivados = false;

    public GameObject imagenHasGanado;

    bool reiniciandoPartida = false;
    void Start()
    {
        estaIniciada = false;

        textos.SetActive(false);

        Cubos = GameObject.FindGameObjectsWithTag(tagObjetos);

        ComportamientoCubos comportamientoCubos = FindObjectOfType<ComportamientoCubos>();
        puntos = comportamientoCubos.puntos;


    }

    // Update is called once per frame
    void Update()
    {
        textoTiempo.text = tiempo.ToString("00");
        textoVida.text = vida.ToString();
        textoPuntos.text = puntos.ToString("000");

        if (estaIniciada)
        {
            LeanTween.moveLocalX(botonIniciar, 250f, velocidad).setEase(LeanTweenType.easeOutSine);
            LeanTween.moveLocalX(botonOpciones, 250f, velocidad).setEase(LeanTweenType.easeOutSine);
            LeanTween.moveLocalX(botonSalir, 250f, velocidad).setEase(LeanTweenType.easeOutSine);

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

            


            todosDesactivados = false;
            imagenHasGanado.SetActive(false);

            reiniciandoPartida = false;
        }

        if (estaContando)
        {
            tiempo += Time.deltaTime;
            
        }

        if (!todosInactivos)
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

            // Si todos los objetos están inactivos, activar la lógica deseada
            if (todosDesactivados)
            {
                todosInactivos = true;
                imagenHasGanado.SetActive(true);
                // Aquí pones lo que quieres hacer cuando todos los objetos estén inactivos
                textoVida.gameObject.SetActive(false);
                estaContando = false;
                MovimientoPelota.moviendo = false;
            }
        }




    }

    public void IniciarPartida()
    {
        estaIniciada = true;
        vida = nuevaVida;

    }

    public void ReiniciarPartida()
    {
        reiniciandoPartida = true;
        textos.SetActive(false);
        puntos = 0;
        tiempo = 0;

    }


}
