using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad del proyectil
    public int daño = 1; // Daño que hace el proyectil
    private Transform jugador; // Referencia al jugador

    private void Start()
    {
        // Obtener la referencia al jugador
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        // Destruir el proyectil después de 3 segundos
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        // Mover el proyectil hacia el jugador
        if (jugador != null)
        {
            Vector2 direccion = (jugador.position - transform.position).normalized;
            transform.Translate(direccion * velocidad * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el proyectil colisiona con el jugador, aplicar daño
        if (collision.CompareTag("Player"))
        {
            // Aplicar daño al jugador
            Playercontroller jugadorController = collision.GetComponent<Playercontroller>();
            if (jugadorController != null)
            {
                jugadorController.RecibirDaño(daño);
            }

            // Destruir el proyectil inmediatamente después de golpear al jugador
            Destroy(gameObject);
        }
    }
}