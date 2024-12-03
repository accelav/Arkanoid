using System;
using UnityEngine;

public class MoviemientoPlataforma : MonoBehaviour
{
    public Gameplay Gameplay; 

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
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Pelota")
        {
            LeanTween.scaleY(gameObject, 0.07f, 0.15f).setLoopPingPong(1).setOnComplete(() => { });
        }
    }
}

