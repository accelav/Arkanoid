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
    
    Vector3 positionPlat = MoviemientoPlataforma.gameObject.transform.localPosition.x;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
  

        if (Input.GetKeyDown(KeyCode.Space))
        {
            moviendo = true;
        }

        if (moviendo)
        {

            transform.position += (movimientoHorizontal + movimientoVertical) * Time.deltaTime * velocidad;
        }
        if (Gameplay.estaIniciada == true)
        {
            LeanTween.alpha(gameObject, 1f, 2f);
        }



    }

    private void OnCollisionEnter(Collision collision)
    {
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
            movimientoVertical = -movimientoVertical;
            gameObject.transform.position = new Vector3(0, 0.15f, 0);

        }
    }

}
