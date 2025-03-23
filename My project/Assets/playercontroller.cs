using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    // Configuración básica
    public float velocidad = 4f;
    public float velocidadSprint = 8f; // Aumenta la velocidad del sprint
    public float duracionSprint = 0.5f;
    public float cooldownSprint = 3f; // Reduzco el cooldown para pruebas
    public int vida = 10;
    public int dañoAtaque = 2;

    // Referencias
    public Animator animator;
    private Rigidbody2D rb;

    private bool estaAtacando = false;
    private bool puedeSprint = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        if (!estaAtacando)
        {
            Movimiento();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !estaAtacando && puedeSprint)
        {
            Sprint();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Atacar();
        }
    }

    private void Movimiento()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoY = Input.GetAxis("Vertical");

        Vector2 movimiento = new Vector2(movimientoX, movimientoY).normalized;

        rb.linearVelocity = movimiento * velocidad; // Usar rb.velocity

        animator.SetFloat("movement", movimiento.magnitude);

        if (movimientoX != 0)
        {
            transform.localScale = new Vector2(Mathf.Sign(movimientoX), 1);
        }
    }

    private void Sprint()
    {
        float movimientoX = Input.GetAxis("Horizontal");
        float movimientoY = Input.GetAxis("Vertical");

        Vector2 direccionSprint = new Vector2(movimientoX, movimientoY).normalized;

        if (direccionSprint.magnitude == 0)
        {
            direccionSprint = new Vector2(transform.localScale.x, 0);
        }

        rb.linearVelocity = direccionSprint * velocidadSprint; // Usar rb.velocity

        animator.SetTrigger("Jump");

        puedeSprint = false;

        Invoke("ReactivarSprint", cooldownSprint);
        Invoke("DetenerSprint", duracionSprint);
    }

    private void DetenerSprint()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x * (velocidad/velocidadSprint) , rb.linearVelocity.y * (velocidad/velocidadSprint));
    }

    private void ReactivarSprint()
    {
        puedeSprint = true;
    }

    private void Atacar()
    {
        animator.SetTrigger("Atacar");
        estaAtacando = true;
        rb.linearVelocity = Vector2.zero; // Detener el movimiento con rb.velocity
        Invoke("TerminarAtaque", 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x > 0 ? Vector2.right : Vector2.left, 1.5f);
        if (hit.collider != null && hit.collider.CompareTag("Enemigo"))
        {
            hit.collider.GetComponent<Enemigo>().RecibirDaño(dañoAtaque);
        }
    }

    private void TerminarAtaque()
    {
        estaAtacando = false;
    }

    public void RecibirDaño(int daño)
    {
        vida -= daño;
        animator.SetTrigger("Daño");

        if (vida <= 0)
        {
            Debug.Log("Jugador ha perdido toda su vida.");
        }
    }
}