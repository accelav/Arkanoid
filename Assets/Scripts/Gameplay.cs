using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{

    [SerializeField]
    GameObject botonIniciar;
    [SerializeField]
    GameObject botonOpciones;

    GameObject esfera;
    GameObject plataforma;
    public bool estaIniciada = false;
    public float velocidad = 0.5f;
    Vector3 posicionInicialPlataforma = Vector3.zero;
    Vector3 posicionInicialEsfera = new Vector3(0, 0.12f, 0);
    float vida = 3;

    void Start()
    {
        estaIniciada = false;

        botonIniciar.transform.localPosition = new Vector3( 0, 0 , 0);
        botonOpciones.transform.localPosition = new Vector3(0 , -34, 0);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (estaIniciada)
        {
            LeanTween.moveLocalX(botonIniciar, 250f, velocidad).setEase(LeanTweenType.easeOutSine);
            LeanTween.moveLocalX(botonOpciones, 250f, velocidad).setEase(LeanTweenType.easeOutSine);

            estaIniciada = false;
        }



    }

    public void IniciarPartida()
    {
        estaIniciada = true;
    }


}
