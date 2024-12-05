using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
public class ComportamientoPowerUps : MonoBehaviour
{
    public Gameplay Gameplay;
    public MovimientoPelota MovimientoPelota;
    public MoviemientoPlataforma MoviemientoPlataforma;
    public ComportamientoCubos ComportamientoCubos;

    Rigidbody rb;

    public bool corazonPowerUp;
    public bool realentizadorPowerUp;
    public bool destructorPowerUp;
    public bool inversionPlataformaPU;
    public bool estaInvirtiendo = false;

    

    [SerializeField]
    AudioClip corazon;
    [SerializeField]
    AudioClip realentizador;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Gameplay = FindObjectOfType<Gameplay>();
        MovimientoPelota = FindObjectOfType<MovimientoPelota>();
        MoviemientoPlataforma = FindObjectOfType<MoviemientoPlataforma>();
        ComportamientoCubos = FindObjectOfType<ComportamientoCubos>();
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
                ControladorDeSonidos.instance.EjecutarSonido(corazon);
                Destroy(gameObject);
            }
        }
        if (realentizadorPowerUp)
        {
            if (other.gameObject.tag == "Plataforma")
            {
  
                MovimientoPelota.velocidad = 0.8f;
                ControladorDeSonidos.instance.EjecutarSonido(realentizador);
                Destroy(gameObject);
                
            }
        }

        if (inversionPlataformaPU)
        {
            if(other.gameObject.tag == "Plataforma")
            {
                MoviemientoPlataforma.estaInvirtiendo = true;
                ControladorDeSonidos.instance.EjecutarSonido(realentizador);
                Destroy(gameObject);
            }
        }
        if (destructorPowerUp)
        {
            ComportamientoCubos.estaDestruyendo = true;
            Destroy(gameObject);
        }
    }
}
