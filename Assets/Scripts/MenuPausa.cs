using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        // Detectar el botón Options del mando de PlayStation
        if (Input.GetKeyDown("joystick button 9"))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f; // Pausa el juego
        isPaused = true;
        Debug.Log("Juego pausado.");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f; // Reanuda el juego
        isPaused = false;
        Debug.Log("Juego reanudado.");
    }
}