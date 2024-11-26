using System;
using UnityEngine;

public class MoviemientoPlataforma : MonoBehaviour
{
    public Gameplay Gameplay; 

    public float limiteDer = 0.95f;   // L�mite derecho
    public float limiteIzq = -0.95f; // L�mite izquierdo
    public float velocidad = 5f;     // Velocidad de la plataforma

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
    }
    void FixedUpdate()
    {

        // Obtener entrada del jugador
        float movimiento = Input.GetAxisRaw("Horizontal") * Time.deltaTime * velocidad;

        // Calcular nueva posici�n basada en la entrada
        Vector3 nuevaPosicion = rb.position + new Vector3(movimiento * Time.fixedDeltaTime, 0, 0);

        // Restringir la posici�n dentro de los l�mites
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzq + escalaPlataforma.x / 2, limiteDer - escalaPlataforma.x / 2);

        // Aplicar la nueva posici�n al Rigidbody
        rb.MovePosition(nuevaPosicion);
    }
}
