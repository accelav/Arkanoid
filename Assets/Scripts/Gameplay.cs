using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
    [SerializeField]
    GameObject esfera;
    [SerializeField]
    GameObject plataforma;
    [SerializeField]
    GameObject botonInicar;
    [SerializeField]
    GameObject bot�nOpciones;
    public bool estaIniciada = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (estaIniciada)
        {
            LeanTween leanTween
        }
    }

    public void IniciarPartida()
    {
        estaIniciada = true;
    }
}