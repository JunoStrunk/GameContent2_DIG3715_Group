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
    Vector3 rot;

    Rigidbody rb;
    SphereCollider groundCol;
    //Transform pivot;
    int groundLayer = 1 << 3;
    // bool isGrounded = true;

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
        groundCol = this.GetComponent<SphereCollider>();
        //pivot = this.transform.parent;
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

        rot = Camera.main.transform.position-transform.position;
        rot.y = 0;
        transform.rotation = Quaternion.LookRotation(rot);
    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.layer == 3) //If player ground collider leaves contact with ground layer
        {
            Collider[] cols = Physics.OverlapSphere( new Vector3(transform.position.x, transform.position.y-0.5f, transform.position.z), 0.6f, groundLayer); //Get ground in contact with player

            if(cols.Length < 1) //If there is no ground in contact with player
            {
                // Debug.Log("Left the ground");
                Ray groundCheckRay = new Ray(transform.position, Vector3.down);
                RaycastHit groundHitInfo;

                if(Physics.Raycast(groundCheckRay, out groundHitInfo, Mathf.Infinity, groundLayer))
                {
                    transform.position = new Vector3(transform.position.x, groundHitInfo.point.y+1f, transform.position.z); //Move player to ground
                    // isGrounded = true;
                }
            }
            
            // isGrounded = false;
        }
    }
}
