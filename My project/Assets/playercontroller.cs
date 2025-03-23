using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    // Configuración básica
    public float velocidad = 5f; // Velocidad de movimiento normal
    public float velocidadSprint = 10f; // Velocidad del sprint
    public float duracionSprint = 0.5f; // Duración del sprint en segundos
    public float cooldownSprint = 10f; // Cooldown del sprint en segundos
    public int vida = 10; // Vida del jugador
    public int dañoAtaque = 5; // Daño que hace el jugador
    public float rangoAtaque = 1.5f; // Rango de ataque del jugador
    public LayerMask capaEnemigos; // Capa para filtrar enemigos

    // Referencias
    public Animator animator; // Referencia al Animator
    private Rigidbody2D rb; // Referencia al Rigidbody2D

    private bool estaAtacando = false; // Para verificar si el personaje está atacando
    private bool puedeSprint = true; // Para controlar el cooldown del sprint

    private void Start()
    {
        // Obtener el componente Rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Desactivar la gravedad
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        // Movimiento (solo si no está atacando)
        if (!estaAtacando)
        {
            Movimiento();
        }

        // Sprint (solo si no está atacando y puede hacer sprint)
        if (Input.GetKeyDown(KeyCode.Space) && !estaAtacando && puedeSprint)
        {
            Sprint();
        }

        // Ataque con clic izquierdo (en cualquier momento)
        if (Input.GetMouseButtonDown(0)) // Clic izquierdo
        {
            Atacar();
        }
    }

    private void Movimiento()
    {
        // Obtener la entrada del jugador (horizontal y vertical)
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoY = Input.GetAxis("Vertical");

        // Crear el vector de movimiento
        Vector2 movimiento = new Vector2(movimientoX, movimientoY).normalized;

        // Mover al jugador
        transform.Translate(movimiento * velocidad * Time.deltaTime);

        // Animación de movimiento
        animator.SetFloat("movement", movimiento.magnitude);

        // Girar el sprite según la dirección del movimiento horizontal
        if (movimientoX != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(movimientoX), 1);
        }
    }

    private void Sprint()
    {
        // Obtener la dirección del movimiento
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoY = Input.GetAxis("Vertical");

        // Crear el vector de dirección del sprint
        Vector2 direccionSprint = new Vector2(movimientoX, movimientoY).normalized;

        // Si no hay dirección de movimiento, sprint hacia la dirección actual del sprite
        if (direccionSprint.magnitude == 0)
        {
            direccionSprint = new Vector2(transform.localScale.x, 0); // Sprint hacia la dirección actual
        }

        // Aplicar fuerza para el sprint
        rb.linearVelocity = direccionSprint * velocidadSprint;

        // Activar la animación de sprint (puedes usar la misma animación que el salto)
        animator.SetTrigger("Jump");

        // Desactivar el sprint temporalmente
        puedeSprint = false;

        // Llamar a un método para reactivar el sprint después del cooldown
        Invoke("ReactivarSprint", cooldownSprint);

        // Llamar a un método para detener el sprint después de la duración
        Invoke("DetenerSprint", duracionSprint);
    }

    private void DetenerSprint()
    {
        // Detener el sprint
        rb.linearVelocity = Vector2.zero;
    }

    private void ReactivarSprint()
    {
        // Reactivar el sprint
        puedeSprint = true;
    }

    private void Atacar()
    {
        // Activar la animación de ataque
        animator.SetTrigger("Atacar");

        // Indicar que el personaje está atacando
        estaAtacando = true;

        // Detener el movimiento mientras ataca
        rb.linearVelocity = Vector2.zero;

        // Llamar a un método para permitir el movimiento después de la animación
        Invoke("TerminarAtaque", 0.5f); // Ajusta el tiempo según la duración de la animación de ataque

        // Detectar enemigos dentro del rango de ataque
        Collider2D[] enemigos = Physics2D.OverlapCircleAll(transform.position, rangoAtaque, capaEnemigos);

        foreach (Collider2D enemigo in enemigos)
        {
            // Aplicar daño al enemigo
            enemigo.GetComponent<Enemigo>().RecibirDaño(dañoAtaque);
            Debug.Log("Ataque golpeó al enemigo: " + enemigo.name);
        }
    }

    private void TerminarAtaque()
    {
        // Indicar que el personaje ha terminado de atacar
        estaAtacando = false;
    }

    public void RecibirDaño(int daño)
    {
        // Reducir la vida
        vida -= daño;

        // Activar la animación de daño
        animator.SetTrigger("Daño");

        // Verificar si el jugador ha perdido toda su vida
        if (vida <= 0)
        {
            Debug.Log("Jugador ha perdido toda su vida.");
            // Aquí puedes agregar lógica adicional, como mostrar un mensaje de derrota.
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Dibujar el rango de ataque en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoAtaque);
    }
}
