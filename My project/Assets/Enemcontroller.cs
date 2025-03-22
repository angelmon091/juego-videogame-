using UnityEngine;
using System;

public class Enemcontroller : MonoBehaviour
{
    public Transform jugador; // Referencia al jugador
    public float velocidad = 2f; // Velocidad de movimiento
    public float rangoDeAtaque = 5f; // Distancia para atacar
    public int daño = 10; // Daño al jugador
    public Animator animator; // Referencia al Animator

    private void Update()
    {
        // Calcular la distancia al jugador
        float distancia = Vector2.Distance(transform.position, jugador.position);

        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        float velocidadY = Input.GetAxis("Vertical") * Time.deltaTime * velocidad;

        animator.SetFloat("movement", Math.Abs(velocidadX * velocidad));

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

        // Moverse hacia el jugador si está fuera del rango de ataque
        if (distancia > rangoDeAtaque)
        {
            MoverHaciaJugador();
            animator.SetBool("isAtacando", false); // Desactiva la animación de ataque
        }
        else
        {
            AtacarJugador();
            
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
        // Lógica para atacar al jugador
        animator.SetBool("isAtacando", true); // Activa la animación de ataque
        Debug.Log("Atacando al jugador, daño: " + daño);
        // Aquí puedes añadir lógica para aplicar daño al jugador
    }
}

