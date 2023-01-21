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
    Vector3 dir;

    Rigidbody rb;

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
        dir = context.ReadValue<Vector3>();
    }

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        rb.velocity = dir * speed;
    }
}
