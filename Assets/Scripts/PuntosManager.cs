using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosManager : MonoBehaviour
{
    public static PuntosManager Instancia { get; private set; }

    private int puntos;
    private int record;
    public int restarCubos = 0;
    public int vecesRoto;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        CargarRecord();
    }

    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad;
        
    }

    public int ObtenerPuntos()
    {
        return puntos;
    }

    public int ObtenerRecord()
    {
        return record;
    }

    public void ReiniciarPuntos()
    {
        if (puntos > record || record == 0)
        {
            record = puntos; // Si el récord es 0, se actualiza automáticamente con los puntos
            GuardarRecord();
        }

        puntos = 0;
    }

    public void ResetearRecord()
    {
        record = 0;
        GuardarRecord();
        //Debug.Log("El récord ha sido reseteado.");
    }

    private void GuardarRecord()
    {
        PlayerPrefs.SetInt("Record", record);
        PlayerPrefs.Save();
    }

    private void CargarRecord()
    {
        record = PlayerPrefs.GetInt("Record", 0);
    }

    public void RestarCubos()
    {
        restarCubos =  restarCubos - 1;
    }

    public void ContarRotura(int n)
    {
        vecesRoto += n;
    }

}