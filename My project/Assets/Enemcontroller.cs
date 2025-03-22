using UnityEngine;

public class Enemcontroller : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float velocidad = 2f; // Velocidad de movimiento
    public float rangoDeAtaque = 1.5f; // Distancia para atacar
    public int daño = 10; // Daño al jugador
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
            animator.SetBool("isAtacando", false); // Desactiva la animación de ataque
            atacando = false; // Resetea el estado de ataque
        }
        else
        {
            if (!atacando) // Solo atacar si no está atacando actualmente
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
        animator.SetBool("isAtacando", true); // Activa la animación de ataque
        atacando = true; // Cambia el estado de ataque

        // Lógica para aplicar daño al jugador (puedes implementar aquí)
        Debug.Log("Atacando al jugador, daño: " + daño);

        // Después de un breve retraso, desactiva la animación de ataque
        Invoke("FinalizarAtaque", 0.5f); // Ajusta el tiempo según la duración de la animación
    }

    private void FinalizarAtaque()
    {
        animator.SetBool("isAtacando", false); // Desactiva la animación de ataque
        atacando = false; // Permite un nuevo ataque
    }
}


