using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public static PowerUps instance;

    Vector3 posicionar;
    public int numero;
    GameObject objetoPowerUp;

    private void Update()
    {
        if (PuntosManager.Instancia.restarCubos == -2)
        {
            //Instantiate(objetoPowerUp, posicionar, Quaternion.identity);
        }
    }

}
