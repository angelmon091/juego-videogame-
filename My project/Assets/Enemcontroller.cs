using UnityEngine;

public class Enemcontroller : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float velocidad = 2f; // Velocidad de movimiento
    public float rangoDeAtaque = 1.5f; // Distancia para atacar
    public int da�o = 10; // Da�o al jugador
    public Animator animator; // Referencia al Animator

    private bool atacando = false; // Para controlar el estado de ataque

    private void Update()
    {
        // Calcular la distancia al jugador
        float distancia = Vector2.Distance(transform.position, jugador.position);

        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        float velocidadY = Input.GetAxis("Vertical") * Time.deltaTime * velocidad;

        animator.SetFloat("movement", velocidadX * velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (velocidadX > 0)
        {
            transform.localScale = new Vector2(1, 1);
        }

        Vector2 posicion = transform.position;

        transform.position = new Vector2(velocidadX + posicion.x, posicion.y + velocidadY);

        if (distancia > rangoDeAtaque)
        {
            MoverHaciaJugador();
            animator.SetBool("isAtacando", false); // Desactiva la animaci�n de ataque
            atacando = false; // Resetea el estado de ataque
        }
        else
        {
            if (!atacando) // Solo atacar si no est� atacando actualmente
            {
                AtacarJugador();
            }
        }
    }

    private void MoverHaciaJugador()
    {
        // Moverse hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, jugador.position, velocidad * Time.deltaTime);
    }

    private void AtacarJugador()
    {
        animator.SetBool("isAtacando", true); // Activa la animaci�n de ataque
        atacando = true; // Cambia el estado de ataque

        // L�gica para aplicar da�o al jugador (puedes implementar aqu�)
        Debug.Log("Atacando al jugador, da�o: " + da�o);

        // Despu�s de un breve retraso, desactiva la animaci�n de ataque
        Invoke("FinalizarAtaque", 0.5f); // Ajusta el tiempo seg�n la duraci�n de la animaci�n
    }

    private void FinalizarAtaque()
    {
        animator.SetBool("isAtacando", false); // Desactiva la animaci�n de ataque
        atacando = false; // Permite un nuevo ataque
    }
}


