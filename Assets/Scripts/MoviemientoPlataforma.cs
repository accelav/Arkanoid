using System;
using UnityEngine;

public class MoviemientoPlataforma : MonoBehaviour
{
    public Gameplay Gameplay;
    public MovimientoPelota MovimientoPelota;

    public float limiteDer = 0.95f;   // Límite derecho
    public float limiteIzq = -0.95f; // Límite izquierdo
    public float velocidad = 100f;     // Velocidad de la plataforma

    GameObject plataforma;

    private Rigidbody rb;
    private Vector3 escalaPlataforma;
    public Vector3 posicionPlataforma;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        escalaPlataforma = transform.localScale;
        posicionPlataforma = transform.localPosition;
    }
    private void Update()
    {
        if (Gameplay.estaIniciada == true)
        {

            LeanTween.alpha(gameObject, 1f, 2f);
            

        }

        if (Gameplay.vida <= 0)
        {
            velocidad = 0;
            LeanTween.alpha(gameObject, 0f, 0.5f).setOnComplete(() => { });
        }
        else
        {
            velocidad = 100f;
        }
    }
    void FixedUpdate()
    {

        
        float movimiento = Input.GetAxisRaw("Horizontal") * Time.deltaTime * velocidad;

        // Calcular nueva posición basada en la entrada
        Vector3 nuevaPosicion = rb.position + new Vector3(movimiento * Time.fixedDeltaTime, 0, 0);

        // Restringir la posición dentro de los límites
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzq + escalaPlataforma.x / 2, limiteDer - escalaPlataforma.x / 2);

        // Aplicar la nueva posición al Rigidbody
        rb.MovePosition(nuevaPosicion);
    }
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Pelota")
        {
            if (MovimientoPelota.moviendo == true)
            {
                LeanTween.scaleZ(gameObject, 0.02f, 0.1f).setLoopPingPong(1).setOnComplete(() => { transform.localScale = new Vector3(0.28f, 0.49f,  0.05f); });
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag =="CorazonPowerUp")
        {
            Debug.Log("corazon Ha Tocado");
        }
    }
}

