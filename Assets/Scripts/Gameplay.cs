using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

    [SerializeField]
    GameObject botonIniciar;
    [SerializeField]
    GameObject botonOpciones;
    
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

    bool reiniciandoPartida = false;
    void Start()
    {
        estaIniciada = false;

        textoTiempo.gameObject.SetActive(false);
        botonIniciar.transform.localPosition = new Vector3( 0, 0 , 0);
        botonOpciones.transform.localPosition = new Vector3(0 , -34, 0);
        textoVida.gameObject.SetActive(false);
        

    }

    // Update is called once per frame
    void Update()
    {
        textoTiempo.text = tiempo.ToString("0:00");
        textoVida.text = vida.ToString();
        if (estaIniciada)
        {
            LeanTween.moveLocalX(botonIniciar, 250f, velocidad).setEase(LeanTweenType.easeOutSine);
            LeanTween.moveLocalX(botonOpciones, 250f, velocidad).setEase(LeanTweenType.easeOutSine);
            textoVida.gameObject.SetActive(true);
            textoTiempo.gameObject.SetActive(true);

          
            estaIniciada = false;

        }

        if (reiniciandoPartida)
        {
            imagenGameOver.SetActive(false);
            LeanTween.moveLocalX(botonIniciar, 0, velocidad).setEase(LeanTweenType.easeOutSine);
            LeanTween.moveLocalX(botonOpciones, 0, velocidad).setEase(LeanTweenType.easeOutSine);
            
            LeanTween.alpha(esfera, 0f, 0.4f);
            LeanTween.alpha(plataforma, 0f, 0.4f);

            textoTiempo.gameObject.SetActive(false);
            tiempo = 0;



            reiniciandoPartida = false;
        }

        if (estaContando)
        {
            tiempo += Time.deltaTime;
            
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
       
    }


}
