using UnityEngine;

public class EnemigoDistancia : MonoBehaviour
{
    // Configuración básica
    public float velocidad = 3f; // Velocidad de movimiento del enemigo
    public int vida = 5; // Vida del enemigo
    public int dañoAtaque = 1; // Daño que hace el enemigo
    public float rangoDeteccion = 5f; // Rango para detectar al jugador
    public float rangoAtaque = 5f; // Rango para atacar al jugador (más grande que el enemigo normal)
    public GameObject proyectilPrefab; // Prefab del proyectil
    public float velocidadProyectil = 10f; // Velocidad del proyectil
    public float tiempoEntreAtaques = 2f; // Tiempo entre ataques
    private float tiempoSiguienteAtaque = 0f; // Tiempo para el siguiente ataque

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
        // Si el enemigo está muerto, no hacer nada
        if (estaMuerto) return;

        // Calcular la distancia al jugador
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador está dentro del rango de detección, moverse hacia él
        if (distanciaAlJugador <= rangoDeteccion)
        {
            MoverseHaciaJugador();

            // Si el jugador está dentro del rango de ataque, atacar
            if (distanciaAlJugador <= rangoAtaque && Time.time >= tiempoSiguienteAtaque)
            {
                Atacar();
                tiempoSiguienteAtaque = Time.time + tiempoEntreAtaques;
            }
        }
        else
        {
            // Desactivar la animación de caminar si el jugador está fuera del rango de detección
            animator.SetBool("Caminando", false);
        }
    }

    private void MoverseHaciaJugador()
    {
        // Calcular la dirección hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;

        // Mover al enemigo hacia el jugador
        transform.Translate(direccion * velocidad * Time.deltaTime);

        // Girar el sprite según la dirección del movimiento
        if (direccion.x != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(direccion.x), 1);
        }

        // Activar la animación de caminar
        animator.SetBool("Caminando", true);
    }

    private void Atacar()
    {
        // Activar la animación de ataque
        animator.SetTrigger("Atacar");

        // Disparar un proyectil hacia el jugador
        DispararProyectil();
    }

    private void DispararProyectil()
    {
        // Crear el proyectil
        GameObject proyectil = Instantiate(proyectilPrefab, transform.position, Quaternion.identity);

        // Obtener el componente Proyectil y configurar su velocidad y daño
        Proyectil scriptProyectil = proyectil.GetComponent<Proyectil>();
        if (scriptProyectil != null)
        {
            scriptProyectil.velocidad = velocidadProyectil;
            scriptProyectil.daño = dañoAtaque;
        }
        else
        {
            Debug.LogError("El prefab del proyectil no tiene el script Proyectil adjunto.");
        }
    }

    public void RecibirDaño(int daño)
    {
        // Reducir la vida
        vida -= daño;

        // Activar la animación de daño solo si el enemigo no ha muerto
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

        // Activar la animación de muerte
        animator.SetTrigger("Muerte");

        // Desactivar el enemigo después de un breve retraso (para que la animación de muerte se reproduzca)
        Invoke("DesactivarEnemigo", 1.5f); // Ajusta el tiempo según la duración de la animación de muerte

        Debug.Log("Enemigo a distancia ha muerto.");
    }

    private void DesactivarEnemigo()
    {
        // Desactivar el enemigo
        gameObject.SetActive(false);
    }
}