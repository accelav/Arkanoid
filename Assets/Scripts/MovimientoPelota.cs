using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovimientoPelota : MonoBehaviour
{
    public Gameplay Gameplay;
    public MoviemientoPlataforma MovimientoPlataforma;
    public ComportamientoCubos ComportamientoCubos;
    public float velocidad = 2f;
    Vector3 movimientoHorizontal = Vector3.right;
    Vector3 movimientoVertical = Vector3.up;
    public bool moviendo = false;
    Rigidbody rb;
    Vector3 vectorPelota = new Vector3(1, 1, 0);
    Vector3 vectorInicial;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
 
    void Update()
    {
        rb.velocity = (movimientoHorizontal + movimientoVertical).normalized * velocidad;
        //Debug.Log("Velocity" + rb.velocity);

        if (Gameplay.estaIniciada == true)
        {
            LeanTween.alpha(gameObject, 1f, 2f);
        }


        float posicionXPlataforma = MovimientoPlataforma.gameObject.transform.position.x;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moviendo = true;
            
            

        }

        
        //rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, -5f, 5f), Mathf.Clamp(rb.velocity.y, 0f, 5f), 0f);



        if (moviendo)
        {

            if (Gameplay.vida <= 0 || Gameplay.todosDesactivados)
            {
                moviendo = false;
            }
            else
            {

                //transform.position += (movimientoHorizontal + movimientoVertical) * Time.deltaTime * velocidad;
                
                Gameplay.estaContando = true;
            }
        }
        
        else
        {
            gameObject.transform.position = new Vector3(posicionXPlataforma, 0.15f, 0);

        }



    }

    private void OnCollisionEnter(Collision collision)
    {
        float posicionXPlataforma = MovimientoPlataforma.gameObject.transform.position.x;
        //Debug.Log("Posicion x plataforma" + posicionXPlataforma);
       
        float posPelota = gameObject.transform.position.x;
        //Debug.Log("posXpelota" + posPelota);

        if (collision.gameObject.tag == "Plataforma")
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                float puntoDeContacto = contact.point.x; // Posición del contacto
                float centroPlataforma = collision.transform.position.x; // Centro de la plataforma

                // Calcula la diferencia normalizada
                float diferencia = puntoDeContacto - centroPlataforma;
                float anchoPlataforma = collision.collider.bounds.size.x / 2; // Mitad del ancho

                float factor = diferencia / anchoPlataforma; // Normaliza la diferencia (-1 a 1)

                // Ajusta el vector de movimiento

                movimientoHorizontal = new Vector3(factor, 0, 0);
                movimientoVertical = new Vector3(0, 1, 0);

                // Combina las direcciones y ajusta la velocidad
                Vector3 nuevaDireccion = (movimientoHorizontal + movimientoVertical).normalized * velocidad;

                // Actualiza los vectores de movimiento
                movimientoHorizontal = new Vector3(nuevaDireccion.x, 0, 0);
                movimientoVertical = new Vector3(0, nuevaDireccion.y, 0);
            }
        }

        else
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Vector3 normal = contact.normal;

                if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
                {
                    movimientoHorizontal = -movimientoHorizontal;

                }
                else if (Mathf.Abs(normal.y) > Mathf.Abs(normal.x))
                {
                    movimientoVertical = -movimientoVertical;

                }
            }
        }

            
        /*if (collision.gameObject.tag == "Cube")
        {
            

          
        }


        if (collision.gameObject.tag == "ParedesLaterales")
        {
            movimientoHorizontal = -movimientoHorizontal;
        }
        if (collision.gameObject.tag == "ParedSuperior")
        {
            movimientoVertical = -movimientoVertical;
        }
        if (collision.gameObject.tag == "Plataforma")
        {
            movimientoVertical = -movimientoVertical;
        }
        /*if (collision.gameObject.tag == "Cube")
        {
            
            //movimientoVertical = -movimientoVertical;
            //movimientoHorizontal = -movimientoHorizontal;
        }*/

        if (collision.gameObject.tag == "ParedAbajo")
        {
            Gameplay.vida = Gameplay.vida - 1;
            

            if (Gameplay.vida <= 0)
            {
                Gameplay.textoVida.gameObject.SetActive(false);
                Gameplay.imagenGameOver.gameObject.SetActive(true);
                gameObject.transform.position = new Vector3(posicionXPlataforma, 0.15f, 0);
                Gameplay.estaContando = false;

                
            }
            else
            {
                Gameplay.imagenGameOver.gameObject.SetActive(false);
            }
            
            //movimientoVertical = -movimientoVertical;
            gameObject.transform.position = new Vector3(posicionXPlataforma, 0.15f, 0);
            
        }

        
    }

}
