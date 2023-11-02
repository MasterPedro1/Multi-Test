using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : FSM
{
    [Header("Player_Settings")]
    public float velocidad = 5.0f;
    [SerializeField] float JumpForce;
    [SerializeField] float attackRadius;
    [SerializeField] bool isplayerDead;
    public enum FSMState
    {
        None, Idle, Moving, Jump, Fall, Attack, Hit, Dead
    }
    public FSMState curState = FSMState.None;

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoX = movimientoHorizontal * velocidad * Time.deltaTime;
        transform.Translate(new Vector3(movimientoX, 0, 0));
    }
    private void FixedUpdate()
    {
        
    }
}
