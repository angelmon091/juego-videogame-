using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu_pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private GameObject menuReiniciar;
    [SerializeField] private AudioMixer audioMixer;

    public void menu_Reinicio()
    {
        menuReiniciar.SetActive(true);
        botonPausa.SetActive(false);
    }

    public void Pausa()
    {
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    public void CambiarVolumen(float volumen)
    {
        audioMixer.SetFloat("volumen", volumen);
    }

    public void Reanudar()
    {
        Time.timeScale = 1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
        menuReiniciar.SetActive(false);
    }

    public void volver_menu()
    {
        if(SceneManager.GetActiveScene().buildIndex > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            Debug.Log("No se puede volver al menu, build index actual es 0");
        }
    }

    public void Cerrar_juego()
    {
        Debug.Log("Cerrando juego...");
        Application.Quit();
    }

    // Funcion para ser llamada por el evento Jugador_muerto del PlayerController
    public void ActivarMenuReiniciar()
    {
        menuReiniciar.SetActive(true);
        botonPausa.SetActive(false);
    }
}