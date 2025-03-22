using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public float velocidad = 5f;

    public bool recibiendoDanio;

    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        animator.SetBool("RecibeDanio", recibiendoDanio)
    }
    public void RecibeDanio(Vector2 direccion, int cantDanio)
    { 
        recibiendoDanio = true;
        Vector2 = new Vector2(transform.position.x - direccion.x, 1).normalized;
        rb.AddForce(rebote* fuerzaRebote, ForceMode2D.impulse);

    }
    public void DesactivaDanio()
    { 
    recibiendoDanio = false;
    }
}
