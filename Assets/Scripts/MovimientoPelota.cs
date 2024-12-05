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

    [SerializeField]
    private AudioClip tocarPared;
    [SerializeField]
    private AudioClip tocarPlataforma;


    public float tiempo;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Gameplay = FindObjectOfType<Gameplay>();
        MovimientoPlataforma = FindObjectOfType<MoviemientoPlataforma>();
        ComportamientoCubos = FindObjectOfType<ComportamientoCubos>();
    }

    // Update is called once per frame
 
    void Update()
    {
        rb.velocity = (movimientoHorizontal + movimientoVertical).normalized * velocidad;

        if (Gameplay.estaIniciada == true)
        {
            LeanTween.alpha(gameObject, 1f, 0.5f);
        }


        float posicionXPlataforma = MovimientoPlataforma.gameObject.transform.position.x;
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("joystick button 1")) && (moviendo == false))
        {
            moviendo = true;

            

        }

        if (moviendo)
        {

            if (Gameplay.vida <= 0)
            {
                moviendo = false;
            }
            else
            {
                Gameplay.estaContando = true;
            }
        }
        
        else
        {
            gameObject.transform.position = new Vector3(posicionXPlataforma, 0.15f, 0);

        }

        if (velocidad < 2f)
        {
            tiempo += Time.deltaTime;
        }
        if (tiempo >= 10f)
        {
            velocidad = 2;
            tiempo = 0f;
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
            ControladorDeSonidos.instance.EjecutarSonido(tocarPlataforma);
            foreach (ContactPoint contact in collision.contacts)
            {
                float puntoDeContacto = contact.point.x;
                float centroPlataforma = collision.transform.position.x; // Centro de la plataforma

                

                float diferencia = puntoDeContacto - centroPlataforma;
                float anchoPlataforma = collision.collider.bounds.size.x / 4;

                float factor = diferencia / anchoPlataforma;

                movimientoHorizontal = new Vector3(factor, 0, 0);
                movimientoVertical = new Vector3(0, 1, 0);

                
                Vector3 nuevaDireccion = (movimientoHorizontal + movimientoVertical).normalized * velocidad;

                
                movimientoHorizontal = new Vector3(nuevaDireccion.x, 0, 0);
                movimientoVertical = new Vector3(0, nuevaDireccion.y, 0);
            }
        }

        else
        {
            ControladorDeSonidos.instance.EjecutarSonido(tocarPared);
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

        if (collision.gameObject.tag == "ParedAbajo")
        {
            Gameplay.vida = Gameplay.vida - 1;
            

            if (Gameplay.vida <= 0)
            {
                gameObject.transform.position = new Vector3(posicionXPlataforma, 0.15f, 0);

                Gameplay.textos.SetActive(false);
                Gameplay.imagenGameOver.gameObject.SetActive(true);  
                Gameplay.estaContando = false;
                Gameplay.canvasPuntosFinales.SetActive(true);
                LeanTween.alpha(gameObject, 0f, 2f).setOnComplete(() => { });


            }
            else
            {
                Gameplay.imagenGameOver.gameObject.SetActive(false);
            }
            
            gameObject.transform.position = new Vector3(posicionXPlataforma, 0.15f, 0);
            
        }

        
    }

}

