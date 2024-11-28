using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObstaculos : MonoBehaviour
{
    public GameObject[] tiposDeObstaculos;  // Prefabs de obst�culos
    public float rangoXMin = -5f;           // Rango m�nimo en X
    public float rangoXMax = 5f;            // Rango m�ximo en X
    public float rangoYMin = -3f;           // Rango m�nimo en Y
    public float rangoYMax = 3f;            // Rango m�ximo en Y

    public int filas = 5;                   // Cantidad de filas
    public int columnas = 5;                // Cantidad de columnas
    public float espaciadoX = 2f;           // Espaciado en X
    public float espaciadoY = 2f;           // Espaciado en Y

    private void Start()
    {
        ColocarObstaculos();  // Llama al m�todo para generar obst�culos cuando el juego comienza
    }

    public void ColocarObstaculos()
    {
        // Asegurarse de que hay prefabs asignados
        if (tiposDeObstaculos.Length == 0)
        {
            Debug.LogError("No se han asignado los prefabs de obst�culos.");
            return;
        }

        for (int fila = 0; fila < filas; fila++)
        {
            for (int columna = 0; columna < columnas; columna++)
            {
                // Calcula las posiciones en X e Y basadas en las filas y columnas
                float posX = rangoXMin + columna * espaciadoX;
                float posY = rangoYMin + fila * espaciadoY;

                // Asegura que las posiciones est�n dentro de los l�mites del mapa
                posX = Mathf.Clamp(posX, rangoXMin, rangoXMax);
                posY = Mathf.Clamp(posY, rangoYMin, rangoYMax);

                // Selecci�n aleatoria del tipo de obst�culo
                GameObject obstaculoSeleccionado = tiposDeObstaculos[Random.Range(0, tiposDeObstaculos.Length)];

                // Instancia el obst�culo en la posici�n calculada
                Instantiate(obstaculoSeleccionado, new Vector3(posX, posY, 0f), Quaternion.identity);
            }
        }
    }
}