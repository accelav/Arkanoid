using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MovimientoPelota : MonoBehaviour
{
    public Gameplay Gameplay;
    public MoviemientoPlataforma MovimientoPlataforma;
    public float velocidad = 2f;
    Vector3 movimientoHorizontal = Vector3.right;
    Vector3 movimientoVertical = Vector3.up;
    bool moviendo = false;
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Gameplay.estaIniciada == true)
        {
            LeanTween.alpha(gameObject, 1f, 2f);
        }


        float posicionXPlataforma = MovimientoPlataforma.gameObject.transform.position.x;
        if (Input.GetKeyDown(KeyCode.Space))
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
                transform.position += (movimientoHorizontal + movimientoVertical) * Time.deltaTime * velocidad;
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
            
            movimientoVertical = -movimientoVertical;
            gameObject.transform.position = new Vector3(posicionXPlataforma, 0.15f, 0);
            
        }
    }

}
