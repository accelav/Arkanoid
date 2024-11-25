using System;
using UnityEngine;

public class MoviemientoPlataforma : MonoBehaviour
{
    public float limiteDer = 0.95f;   // Límite derecho
    public float limiteIzq = -0.95f; // Límite izquierdo
    public float velocidad = 5f;     // Velocidad de la plataforma

    private Rigidbody rb;
    private Vector3 escalaPlataforma;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        escalaPlataforma = transform.localScale;
    }

    void FixedUpdate()
    {
        // Obtener entrada del jugador
        float movimiento = Input.GetAxisRaw("Horizontal") * velocidad;

        // Calcular nueva posición basada en la entrada
        Vector3 nuevaPosicion = rb.position + new Vector3(movimiento * Time.fixedDeltaTime, 0, 0);

        // Restringir la posición dentro de los límites
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, limiteIzq + escalaPlataforma.x / 2, limiteDer - escalaPlataforma.x / 2);

        // Aplicar la nueva posición al Rigidbody
        rb.MovePosition(nuevaPosicion);
    }
}
