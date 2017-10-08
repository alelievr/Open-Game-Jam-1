using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Stopmoving {

	public float					maxSpeed = 1f;
	[HideInInspector] public bool	facingRight = false;

	[Space]
	public float		jumpPower = 10f;
	public float		jumpIdle = .3f;
	bool				canJump = true;

	[Space]
	public float		minSlideVelocity = 3f;

	public int			life = 5;
	public float		timeInvuAfterOuch = 0.7f;
	public float		ouchtime = 0.2f;
	public float		ouchbacklash = 1f;
	float				timesinceouch = 100;
	float				invutime = 0;
	bool				sliding = false;

	bool				istapping = false;
	public Collider2D	zonebam;
	float				timesincetapping;

	bool				grounded;
	public Vector3		groundPosition;
	public Vector2		groundSize;
	
	new Rigidbody2D	rigidbody2D;
	SpriteRenderer	spriteRenderer;
	Animator		anim;
	
	float			tmp;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent< SpriteRenderer >();
		rigidbody2D = GetComponent< Rigidbody2D >();

		rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
		anim = GetComponent< Animator >();
		Flip();
		// anim.SetBool("facingright", facingRight);
		anim.SetBool("grounded", grounded);
	}

	void FixedUpdate()
	{
		float move;
		
		move = Input.GetAxis("Horizontal");

		timesinceouch += Time.deltaTime;
		invutime += Time.deltaTime;
		if (timesinceouch < ouchtime)
			return ;
		else
			anim.SetBool("ouch", false);
		if (base.cannotmove == true)
			return ;

		Tapping(move);

		GroundCheck();

		Move(move);

		SlideCheck();

		anim.SetFloat("vely", rigidbody2D.velocity.y);

		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
	}

	void Tapping(float move)
	{
		if (istapping)
		{
			move = transform.position.x - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane)).x;
			if (!istapping && move > 0 && !facingRight)
				Flip();
			else if (!istapping && move < 0 && facingRight)
				Flip();

			timesincetapping += Time.deltaTime;
			if (timesincetapping > 0.3f)
			{
				istapping = false;
				anim.SetBool("istapping", false);
			}
			else if (timesincetapping > 0.2f)
				zonebam.gameObject.SetActive(false);
			else if (timesincetapping > 0.1f)
				zonebam.gameObject.SetActive(true);
		}
		if (!istapping  && Input.GetKey(KeyCode.Mouse1))
		{
			istapping = true;
			timesincetapping = 0;
			anim.SetBool("istapping", true);
		}
	}

	void Move(float move)
	{
		if (!istapping && move > 0 && !facingRight)
			Flip();
		else if (!istapping && move < 0 && facingRight)
			Flip();
		if (move != 0)
			anim.SetBool("moving", true);
		else
			anim.SetBool("moving", false);	
	}

	void GroundCheck()
	{
		RaycastHit2D[] results = new RaycastHit2D[10];
		int collisionNumber = Physics2D.BoxCastNonAlloc(transform.position + groundPosition, groundSize, 0, Vector2.down, results, .0f, 1 << LayerMask.NameToLayer("Ground"));

		grounded = collisionNumber != 0;

		anim.SetBool("grounded", grounded);
	}

	void SlideCheck()
	{
		float arx = Mathf.Abs(rigidbody2D.velocity.x);
		if (arx > minSlideVelocity && Input.GetKeyDown(KeyCode.DownArrow))
			sliding = true;
		if (arx < minSlideVelocity || !grounded)
			sliding = false;
		
		anim.SetBool("sliding", sliding);
	}

	void Flip()
	{
		facingRight = !facingRight;
		anim.SetBool("facingright", facingRight);
		spriteRenderer.flipX = facingRight;
	}

	void Die()
	{
		anim.SetBool("death", true);
		Destroy(gameObject, 1);
			
	}

	void ouch()
	{
		life--;
		if (life < 1)
			Die();
		else
			anim.SetBool("ouch", true);
		timesinceouch = 0;

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
		if (timesinceouch < ouchtime)
			return ;
		if (grounded && Input.GetKeyDown(KeyCode.Space) && canJump)
		{
			rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
			StartCoroutine(JumpDelay());
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (invutime > timeInvuAfterOuch && other.tag == "ouch")
			ouch();
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(transform.position + groundPosition, groundSize);
	}
}
