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
        //These variables determine if camera is in front/behind, to the left/right of the player
        // Left, Behind - Negative
        //float FB = Vector3.Dot(camera.transform.position - transform.position.normalized, transform.TransformDirection(Vector3.forward).normalized);
        //float LR = Vector3.Dot(camera.transform.position - transform.position.normalized, transform.TransformDirection(Vector3.right).normalized);

        //Debug.Log("FB: " + FB);
        //Debug.Log("LR: " + LR);

        Vector3 forwardRel = dir.z * Camera.main.transform.forward.normalized;
        Vector3 rightRel = dir.x * Camera.main.transform.right.normalized;
        forwardRel.y = 0;
        rightRel.y = 0;

        Vector3 movement = forwardRel + rightRel;

        //transform.Translate(movement, Space.World);

        rb.AddForce(movement * speed);
    }
}
