using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : FSM
{
    public float velocidadMovimiento = 5f; // Velocidad de movimiento del jugador
    public float fuerzaSalto = 10f; // Fuerza de salto
    private bool enSuelo = false; // Verificar si el jugador está en el suelo
    private Rigidbody2D rb;
    private bool isLocalPlayer = true;
    public Animator animator;
    private bool isWalking = false;
    public Slider health;
    private float vida = 100;


    private void Update()
    {
        health.value = vida;
    }

    protected override void initialize()
    {
        rb = GetComponent <Rigidbody2D>();
        
    }


    protected override void FSMUpdate()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        else
        {
            
            // Mover el jugador
            MoverJugador();

            // Saltar si se presiona la tecla de espacio y el jugador está en el suelo
            if (enSuelo && Input.GetKeyDown(KeyCode.Space))
            {
                Saltar();
                animator.Play("Jump");
            }

            if(!enSuelo)
            {
                animator.Play("Jump");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el jugador está en contacto con la capa "Suelo"
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suelo"))
        {
            enSuelo = true;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemigo"))
            vida = vida - 10;
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
        if (movimientoHorizontal != 0)
            animator.Play("Walk");
      
    }

    void Saltar()
    {
        rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
    }
}
