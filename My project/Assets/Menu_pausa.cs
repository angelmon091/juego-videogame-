using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Menu_pausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuPausa;

    [SerializeField] private AudioMixer audioMixer;
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
        Time.timeScale =1f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }

    public void Reiniciar()
    {
        Time.timeScale =1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
    public void volver_menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Cerrar_juego()
    {
        Debug.Log("Cerrando juego...");
        Application.Quit();
    }
}

