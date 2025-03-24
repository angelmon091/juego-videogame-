using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad del proyectil
    public int da�o = 1; // Da�o que hace el proyectil
    private Transform jugador; // Referencia al jugador

    private void Start()
    {
        // Obtener la referencia al jugador
        jugador = GameObject.FindGameObjectWithTag("Player").transform;

        // Destruir el proyectil despu�s de 3 segundos
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
        // Si el proyectil colisiona con el jugador, aplicar da�o
        if (collision.CompareTag("Player"))
        {
            // Aplicar da�o al jugador
            Playercontroller jugadorController = collision.GetComponent<Playercontroller>();
            if (jugadorController != null)
            {
                jugadorController.RecibirDa�o(da�o);
            }

            // Destruir el proyectil inmediatamente despu�s de golpear al jugador
            Destroy(gameObject);
        }
    }
}