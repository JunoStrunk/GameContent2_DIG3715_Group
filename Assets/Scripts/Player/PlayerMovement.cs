using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	// Public Variables ==============
	public float speed = 0f;
	public bool disabled = false;

	// Private Variables =============
	PlayerControls inputControls;
	Vector3 dir;
	Vector3 rot;

	Rigidbody rb;
	Animator anim;
	SphereCollider groundCol;
	//Transform pivot;
	int groundLayer = 1 << 3;
	// bool isGrounded = true;
	float trueSpeed = 0f;

	private void Awake()
	{
		inputControls = new PlayerControls();
		trueSpeed = speed;
	}

	private void OnEnable()
	{
		inputControls.Enable();

		inputControls.Ground.Move.performed += Move;
		inputControls.Ground.Move.started += Move;
		inputControls.Ground.Move.canceled += Move;

		StartCoroutine(DelayedStart());
	}

	private void OnDisable()
	{
		inputControls.Disable();

		inputControls.Ground.Move.performed -= Move;
		inputControls.Ground.Move.started -= Move;
		inputControls.Ground.Move.canceled -= Move;
		GameEventSys.current.onPlayerHides -= PlayerHidden;
	}

	private void Move(InputAction.CallbackContext context)
	{
		dir = context.ReadValue<Vector3>();
	}

	private void Start()
	{
		rb = this.GetComponent<Rigidbody>();
		anim = this.GetComponent<Animator>();
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

		// Debug.Log(forwardRel);
		anim.SetFloat("Forward", dir.z);

		Vector3 movement = forwardRel + rightRel;
		if (movement != Vector3.zero)
			anim.SetBool("Moving", true);
		else
			anim.SetBool("Moving", false);

		//transform.Translate(movement, Space.World);

		if (!disabled)
			rb.AddForce(movement * speed);

		rot = Camera.main.transform.position - transform.position;
		rot.y = 0;
		transform.rotation = Quaternion.LookRotation(rot);
	}

	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.layer == 3) //If player ground collider leaves contact with ground layer
		{
			Collider[] cols = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 2f, transform.position.z), 1f, groundLayer); //Get ground in contact with player

			// Debug.Log(cols.Length < 1);
			if (cols.Length < 1) //If there is no ground in contact with player
			{
				// Debug.Log("Left the ground");
				Ray groundCheckRay = new Ray(transform.position, Vector3.down);
				RaycastHit groundHitInfo;

				if (Physics.Raycast(groundCheckRay, out groundHitInfo, Mathf.Infinity, groundLayer))
				{
					transform.position = new Vector3(transform.position.x, groundHitInfo.point.y + 2f, transform.position.z); //Move player to ground
																															  // isGrounded = true;
				}
			}

			// isGrounded = false;
		}
	}

	void PlayerHidden(bool hidden)
	{
		anim.SetBool("Hidden", hidden);
		if (hidden)
			disabled = true;
		else
			disabled = false;
	}

	public void SlowSpeed()
	{
		speed /= 3;
	}

	public void ResetSpeed()
	{
		speed = trueSpeed;
	}

	IEnumerator DelayedStart()
	{
		yield return new WaitForSeconds(0.2f);
		GameEventSys.current.onPlayerHides += PlayerHidden;
	}

	// void OnDrawGizmos()
	// {
	// 	Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - 2.5f, transform.position.z), 1f);
	// }
}

