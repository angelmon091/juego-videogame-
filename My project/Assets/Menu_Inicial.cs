using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Inicial : MonoBehaviour
{
    public void Jugar(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale =1f;
    }

    public void Salir(){
        Debug.Log("Salir...)");
        Application.Quit();

    }
}

