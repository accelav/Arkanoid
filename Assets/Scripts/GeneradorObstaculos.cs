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
    public int n = 0;
    public GameObject obstaculoSeleccionado;
    private List<GameObject> obst�culosInstanciados = new List<GameObject>();  // Lista para almacenar los obst�culos
    private bool obst�culosGenerados = false;  // Controla si los obst�culos ya han sido generados

    private void Start()
    {
        // Inicializa la lista de obst�culos si es necesario
        // ColocarObstaculos();  // Llama al m�todo para generar obst�culos cuando el juego comienza
    }

    public void ColocarObstaculos()
    {
        // Solo generar obst�culos si no han sido generados previamente
        if (obst�culosGenerados) return;

        DestruirObstaculos();  // Destruir cualquier obst�culo que ya est� instanciado

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

                Vector3 posicion = new Vector3(posX, posY, 0f); // Coordenadas 3D

                // Define el tama�o y orientaci�n de la caja imaginaria para la detecci�n
                Vector3 size = new Vector3(espaciadoX * 0.9f, espaciadoY * 0.9f, 1f); // Tama�o de la caja
                Quaternion orientation = Quaternion.identity; // Sin rotaci�n

                // Verifica si ya hay un objeto en la posici�n con Physics.OverlapBox
                Collider[] colisiones = Physics.OverlapBox(posicion, size / 2, orientation);

                if (colisiones.Length == 0) // Si no hay colisiones, instancia el objeto
                {
                    // Selecci�n aleatoria del tipo de obst�culo
                    obstaculoSeleccionado = tiposDeObstaculos[Random.Range(0, tiposDeObstaculos.Length)];

                    // Instancia el obst�culo en la posici�n calculada
                    GameObject obstaculoInstanciado = Instantiate(obstaculoSeleccionado, posicion, Quaternion.identity);

                    // Agregar el obst�culo instanciado a la lista
                    obst�culosInstanciados.Add(obstaculoInstanciado);

                    n++;
                }
                else
                {
                    Debug.Log($"Ya existe un objeto en la posici�n {posicion}. No se crea uno nuevo.");
                }
            }
        }

        // Establecer que los obst�culos han sido generados
        obst�culosGenerados = true;
    }

    // M�todo para destruir todos los obst�culos instanciados
    public void DestruirObstaculos()
    {
        foreach (GameObject obstaculo in obst�culosInstanciados)
        {
            Destroy(obstaculo);
        }

        // Limpiar la lista despu�s de destruir los obst�culos
        obst�culosInstanciados.Clear();
        obst�culosGenerados = false;  // Marcar que los obst�culos han sido destruidos
    }

    // Este m�todo se puede llamar cuando se presiona el bot�n de "reiniciar" o cualquier otro momento
    public void ReiniciarObstaculos()
    {
        DestruirObstaculos(); // Destruir los obst�culos existentes
        ColocarObstaculos();  // Colocar nuevos obst�culos
    }
}