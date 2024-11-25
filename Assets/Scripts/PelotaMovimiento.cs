using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelotaMovimiento : MonoBehaviour
{
    public float velocidad = 2f;  // Velocidad de la pelota
    public Vector3 direccionInicial = new Vector3(1, 1, 0); // Dirección inicial cuando la pelota es disparada
    public float limiteSup = 0.1f;   // Límite superior de la pantalla
    public float limiteDer = 0.1f;    // Límite derecho
    public float limiteIzq = -0.1f; // Límite izquierdo
    public GameObject plataforma;   // Referencia a la plataforma

    private Rigidbody rb;
    Vector3 escalaPelota;
    private bool disparada = false;  // Variable para comprobar si la pelota ha sido disparada

    void Start()
    {
        escalaPelota = transform.localScale; // Obtiene la escala de la pelota
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;  // Desactivar la gravedad
        rb.velocity = Vector3.zero; // Al principio la pelota está parada
    }

    void Update()
    {
        // Comprobar si se presiona el botón "Espacio"
        if (Input.GetKeyDown(KeyCode.Space) && !disparada)
        {
            disparada = true;  // Marcar que la pelota ha sido disparada
            // Aplicar la velocidad en la dirección inicial (disparo)
            rb.velocity = direccionInicial.normalized * velocidad;
        }

        // Limitar el movimiento en los bordes de la pantalla (Rebote con los límites superiores y laterales)
        Vector3 posicion = rb.position;

        if (posicion.x >= limiteDer - (escalaPelota.x / 2) || posicion.x <= limiteIzq + (escalaPelota.x / 2))
        {
            // Invertir la dirección en el eje X
            rb.velocity = new Vector3(-rb.velocity.x, rb.velocity.y, rb.velocity.z);
            // Asegurarse de que la pelota no se pase del límite
            if (posicion.x >= limiteDer - (escalaPelota.x / 2))
                rb.position = new Vector3(limiteDer - (escalaPelota.x / 2), rb.position.y, rb.position.z);
            if (posicion.x <= limiteIzq + (escalaPelota.x / 2))
                rb.position = new Vector3(limiteIzq + (escalaPelota.x / 2), rb.position.y, rb.position.z);
        }

        if (posicion.y >= limiteSup - (escalaPelota.y / 2))
        {
            // Invertir la dirección en el eje Y al tocar el límite superior
            rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, rb.velocity.z);
            // Asegurarse de que la pelota no se pase del límite
            rb.position = new Vector3(rb.position.x, limiteSup - (escalaPelota.y / 2), rb.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plataforma"))
        {
            Debug.Log("Rebote en la plataforma");

            // Calcular la dirección del rebote usando la normal del impacto
            Vector3 normal = collision.contacts[0].normal;
            rb.velocity = Vector3.Reflect(rb.velocity, normal).normalized * velocidad; // Velocidad constante
        }
        else
        {
            // Para otros objetos: Rebote con velocidad constante
            Vector3 normal = collision.contacts[0].normal;
            rb.velocity = Vector3.Reflect(rb.velocity, normal).normalized * velocidad;
        }
    }
}
