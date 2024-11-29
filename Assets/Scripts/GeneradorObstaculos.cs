using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorObstaculos : MonoBehaviour
{
    public GameObject[] tiposDeObstaculos;  // Prefabs de obstáculos
    public float rangoXMin = -5f;           // Rango mínimo en X
    public float rangoXMax = 5f;            // Rango máximo en X
    public float rangoYMin = -3f;           // Rango mínimo en Y
    public float rangoYMax = 3f;            // Rango máximo en Y

    public int filas = 5;                   // Cantidad de filas
    public int columnas = 5;                // Cantidad de columnas
    public float espaciadoX = 2f;           // Espaciado en X
    public float espaciadoY = 2f;           // Espaciado en Y
    public int n = 0;
    public GameObject obstaculoSeleccionado;
    private List<GameObject> obstáculosInstanciados = new List<GameObject>();  // Lista para almacenar los obstáculos
    private bool obstáculosGenerados = false;  // Controla si los obstáculos ya han sido generados

    private void Start()
    {
        // Inicializa la lista de obstáculos si es necesario
        // ColocarObstaculos();  // Llama al método para generar obstáculos cuando el juego comienza
    }

    public void ColocarObstaculos()
    {
        // Solo generar obstáculos si no han sido generados previamente
        if (obstáculosGenerados) return;

        DestruirObstaculos();  // Destruir cualquier obstáculo que ya esté instanciado

        // Asegurarse de que hay prefabs asignados
        if (tiposDeObstaculos.Length == 0)
        {
            Debug.LogError("No se han asignado los prefabs de obstáculos.");
            return;
        }

        for (int fila = 0; fila < filas; fila++)
        {
            for (int columna = 0; columna < columnas; columna++)
            {
                // Calcula las posiciones en X e Y basadas en las filas y columnas
                float posX = rangoXMin + columna * espaciadoX;
                float posY = rangoYMin + fila * espaciadoY;

                // Asegura que las posiciones están dentro de los límites del mapa
                posX = Mathf.Clamp(posX, rangoXMin, rangoXMax);
                posY = Mathf.Clamp(posY, rangoYMin, rangoYMax);

                Vector3 posicion = new Vector3(posX, posY, 0f); // Coordenadas 3D

                // Define el tamaño y orientación de la caja imaginaria para la detección
                Vector3 size = new Vector3(espaciadoX * 0.9f, espaciadoY * 0.9f, 1f); // Tamaño de la caja
                Quaternion orientation = Quaternion.identity; // Sin rotación

                // Verifica si ya hay un objeto en la posición con Physics.OverlapBox
                Collider[] colisiones = Physics.OverlapBox(posicion, size / 2, orientation);

                if (colisiones.Length == 0) // Si no hay colisiones, instancia el objeto
                {
                    // Selección aleatoria del tipo de obstáculo
                    obstaculoSeleccionado = tiposDeObstaculos[Random.Range(0, tiposDeObstaculos.Length)];

                    // Instancia el obstáculo en la posición calculada
                    GameObject obstaculoInstanciado = Instantiate(obstaculoSeleccionado, posicion, Quaternion.identity);

                    // Agregar el obstáculo instanciado a la lista
                    obstáculosInstanciados.Add(obstaculoInstanciado);

                    n++;
                }
                else
                {
                    Debug.Log($"Ya existe un objeto en la posición {posicion}. No se crea uno nuevo.");
                }
            }
        }

        // Establecer que los obstáculos han sido generados
        obstáculosGenerados = true;
    }

    // Método para destruir todos los obstáculos instanciados
    public void DestruirObstaculos()
    {
        foreach (GameObject obstaculo in obstáculosInstanciados)
        {
            Destroy(obstaculo);
        }

        // Limpiar la lista después de destruir los obstáculos
        obstáculosInstanciados.Clear();
        obstáculosGenerados = false;  // Marcar que los obstáculos han sido destruidos
    }

    // Este método se puede llamar cuando se presiona el botón de "reiniciar" o cualquier otro momento
    public void ReiniciarObstaculos()
    {
        DestruirObstaculos(); // Destruir los obstáculos existentes
        ColocarObstaculos();  // Colocar nuevos obstáculos
    }
}