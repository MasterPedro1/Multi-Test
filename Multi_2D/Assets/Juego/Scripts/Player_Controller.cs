using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : FSM
{
    public float velocidadMovimiento = 5f; // Velocidad de movimiento del jugador
    public float fuerzaSalto = 10f; // Fuerza de salto
    private bool enSuelo = false; // Verificar si el jugador está en el suelo
    private Rigidbody2D rb;

    protected override void initialize()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void FSMUpdate()
    {
        // Mover el jugador
        MoverJugador();

        // Saltar si se presiona la tecla de espacio y el jugador está en el suelo
        if (enSuelo && Input.GetKeyDown(KeyCode.Space))
        {
            Saltar();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el jugador está en contacto con la capa "Suelo"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            enSuelo = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Verificar si el jugador ya no está en contacto con la capa "Suelo"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            enSuelo = false;
        }
    }

    void MoverJugador()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        Vector2 velocidad = rb.velocity;
        velocidad.x = movimientoHorizontal * velocidadMovimiento;
        rb.velocity = velocidad;
    }

    void Saltar()
    {
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
    }
}
