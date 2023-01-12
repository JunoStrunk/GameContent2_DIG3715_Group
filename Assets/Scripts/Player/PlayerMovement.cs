using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Public Variables ==============
    public float speed = 0f;

    // Private Variables =============

    PlayerControls inputControls;
    float dir = 0f;

    Rigidbody2D _rb;

    private void Awake()
    {
        inputControls = new PlayerControls();
    }

    private void OnEnable()
    {
        inputControls.Enable();

        inputControls.Ground.Move.performed += Move;
        inputControls.Ground.Move.started += Move;
        inputControls.Ground.Move.canceled += Move;
    }

    private void OnDisable()
    {
        inputControls.Disable();

        inputControls.Ground.Move.performed -= Move;
        inputControls.Ground.Move.started -= Move;
        inputControls.Ground.Move.canceled -= Move;
    }

    private void Move(InputAction.CallbackContext context)
    {
        context.ReadValue<float>();
        context.ReadValueAsButton();
        dir = context.ReadValue<float>();
    }

    private void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(dir * speed, _rb.velocity.y);
    }
}
