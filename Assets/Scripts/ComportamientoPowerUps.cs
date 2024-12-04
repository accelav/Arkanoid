using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
public class ComportamientoPowerUps : MonoBehaviour
{
    public Gameplay Gameplay;
    public MovimientoPelota MovimientoPelota;

    Rigidbody rb;

    public bool corazonPowerUp;
    public bool realentizadorPowerUp;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Gameplay = FindObjectOfType<Gameplay>();
        MovimientoPelota = FindObjectOfType<MovimientoPelota>();
    }

    void Update()
    {
        rb.velocity = rb.velocity * Time.deltaTime * 0.2f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (corazonPowerUp)
        {
            if (other.gameObject.tag == "Plataforma")
            {
                Gameplay.vida++;
                PuntosManager.Instancia.SumarPuntos(723);
                Destroy(gameObject);
            }
        }
        if (realentizadorPowerUp)
        {
            if (other.gameObject.tag == "Plataforma")
            {
  
                MovimientoPelota.velocidad = 0.8f;
                Destroy(gameObject);
                
            }
        }
    }
}
