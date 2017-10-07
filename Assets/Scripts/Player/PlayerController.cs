using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Stopmoving {

	public float					maxSpeed = 1f;
	[HideInInspector] public bool	facingRight = true;

	public GameObject eyesright;
	public GameObject eyesleft;

	[Space]
	public Transform	groundCheck;
	public LayerMask	groundMask;
	bool				grounded = false;
	public float		groundRadius = .2f;

	[Space]
	public float		jumpPower = 10f;
	public float		jumpIdle = .3f;
	bool				canJump = true;
	
	new Rigidbody2D	rigidbody2D;
	SpriteRenderer	spriteRenderer;
	

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent< SpriteRenderer >();
		rigidbody2D = GetComponent< Rigidbody2D >();
	}

	void FixedUpdate()
	{
		if (base.cannotmove == true)
			return ;
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);

		float move = Input.GetAxis("Horizontal");

		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight)
			Flip();
		else if (move < 0 && facingRight)
			Flip();
	}

	void Flip()
	{
		if (!spriteRenderer)
		{
			eyesright.SetActive(!eyesright.activeInHierarchy);
			eyesleft.SetActive(!eyesright.activeInHierarchy);
			facingRight = !facingRight;
			return;
		}
		facingRight = !facingRight;
		spriteRenderer.flipY = facingRight;
	}

	IEnumerator JumpDelay()
	{
		canJump = false;
		yield return new WaitForSeconds(jumpIdle);
		canJump = true;
	}
	
	void Update () {
		if (base.cannotmove == true)
			return ;
		if (grounded && Input.GetKeyDown(KeyCode.Space) && canJump)
		{
			rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
			StartCoroutine(JumpDelay());
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
	}
}
