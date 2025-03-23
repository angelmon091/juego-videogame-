using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Configuración básica
    public float velocidad = 3f; // Velocidad de movimiento del enemigo
    public int vida = 5; // Vida del enemigo
    public int dañoAtaque = 1; // Daño que hace el enemigo
    public float rangoDeteccion = 10f; // Rango para detectar al jugador
    public float rangoAtaque = 2f; // Rango para atacar al jugador

    // Referencias
    public Animator animator; // Referencia al Animator
    private Transform jugador; // Referencia al jugador

    private void Start()
    {
        // Obtener la referencia al jugador
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Calcular la distancia al jugador
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador está dentro del rango de detección, moverse hacia él
        if (distanciaAlJugador <= rangoDeteccion)
        {
            MoverseHaciaJugador();

            // Si el jugador está dentro del rango de ataque, atacar
            if (distanciaAlJugador <= rangoAtaque)
            {
                Atacar();
            }
            else
            {
                // Desactivar la animación de ataque si el jugador está fuera del rango de ataque
                animator.SetBool("Atacando", false);
            }
        }
        else
        {
            // Desactivar la animación de caminar si el jugador está fuera del rango de detección
            animator.SetBool("Caminandom", false);
        }
    }

    private void MoverseHaciaJugador()
    {
        // Calcular la dirección hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Mover al enemigo hacia el jugadorjugadorjugadorjugadorjugadorjuga
        transform.Translate(direccion * velocidad * Time.deltaTime);

        // Girar el sprite según la dirección del movimiento
        if (direccion.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(direccion.x), 1);
        }

        // Activar la animación de caminar
        animator.SetBool("Caminandom", true);
    }

    private void Atacar()
    {
        // Activar la animación de ataque
        animator.SetBool("Atacando", true);

        // Aplicar daño al jugador
        jugador.GetComponent<Playercontroller>().RecibirDaño(dañoAtaque);
    }

    public void RecibirDaño(int daño)
    {
        // Reducir la vida
        vida -= daño;

        // Activar la animación de daño
        animator.SetTrigger("Danio");

        // Verificar si el enemigo ha muerto
        if (vida <= 0)
        {
            Morir();
        }
    }

    private void Morir()
    {
        // Activar la animación de muerte
        animator.SetTrigger("Muerte");

        // Desactivar el enemigo o eliminarlo del juego
        Debug.Log("Enemigo ha muerto.");
        gameObject.SetActive(false);
    }
}