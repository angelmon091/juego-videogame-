using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Configuraci�n b�sica
    public float velocidad = 3f; // Velocidad de movimiento del enemigo
    public int vida = 5; // Vida del enemigo
    public int da�oAtaque = 1; // Da�o que hace el enemigo
    public float rangoDeteccion = 5f; // Rango para detectar al jugador
    public float rangoAtaque = 1.5f; // Rango para atacar al jugador

    // Referencias
    public Animator animator; // Referencia al Animator
    private Transform jugador; // Referencia al jugador

    private bool estaMuerto = false; // Para verificar si el enemigo ha muerto

    private void Start()
    {
        // Obtener la referencia al jugador
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Si el enemigo est� muerto, no hacer nada
        if (estaMuerto) return;

        // Calcular la distancia al jugador
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador est� dentro del rango de detecci�n, moverse hacia �l
        if (distanciaAlJugador <= rangoDeteccion)
        {
            MoverseHaciaJugador();

            // Si el jugador est� dentro del rango de ataque, atacar
            if (distanciaAlJugador <= rangoAtaque)
            {
                Atacar();
            }
            else
            {
                // Desactivar la animaci�n de ataque si el jugador est� fuera del rango de ataque
                animator.SetBool("Atacando", false);
            }
        }
        else
        {
            // Desactivar la animaci�n de caminar si el jugador est� fuera del rango de detecci�n
            animator.SetBool("Caminandom", false);
        }
    }

    private void MoverseHaciaJugador()
    {
        // Calcular la direcci�n hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Mover al enemigo hacia el jugador
        transform.Translate(direccion * velocidad * Time.deltaTime);

        // Girar el sprite seg�n la direcci�n del movimiento
        if (direccion.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(direccion.x), 1);
        }

        // Activar la animaci�n de caminar
        animator.SetBool("Caminandom", true);
    }

    private void Atacar()
    {
        // Activar la animaci�n de ataque
        animator.SetBool("Atacando", true);

        // Aplicar da�o al jugador
        jugador.GetComponent<Playercontroller>().RecibirDa�o(da�oAtaque);
    }

    public void RecibirDa�o(int da�o)
    {
        // Reducir la vida
        vida -= da�o;

        // Activar la animaci�n de da�o solo si el enemigo no ha muerto
        if (vida > 0)
        {
            animator.SetTrigger("Danio");
        }

        // Verificar si el enemigo ha muerto
        if (vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        // Marcar que el enemigo ha muerto
        estaMuerto = true;

        // Activar la animaci�n de muerte
        animator.SetTrigger("Muerte");

        // Desactivar el enemigo despu�s de un breve retraso (para que la animaci�n de muerte se reproduzca)
        Invoke("DesactivarPato", 1.5f); // Ajusta el tiempo seg�n la duraci�n de la animaci�n de muerte

        Debug.Log("Enemigo ha muerto.");
    }

    private void DesactivarPato()
    {
        // Desactivar el enemigo
        gameObject.SetActive(false);
    }
}