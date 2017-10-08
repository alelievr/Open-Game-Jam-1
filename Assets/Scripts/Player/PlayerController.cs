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
	public float		maxYVelocity = 8f;
	public float		minYVelocity = -6f;
	bool				canJump = true;

	[Space]
	public float		minSlideVelocity = 3f;

	[Space]
	public float		slimeVelocityIgnore = .5f;

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
	public AudioClip	run;
	
	new Rigidbody2D	rigidbody2D;
	SpriteRenderer	spriteRenderer;
	Animator		anim;
	AudioSource		audiosource;
	
	float			tmp;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent< SpriteRenderer >();
		rigidbody2D = GetComponent< Rigidbody2D >();

		rigidbody2D.interpolation = RigidbodyInterpolation2D.Interpolate;
		anim = GetComponent< Animator >();
		audiosource = GetComponent< AudioSource >();
		Flip();
		// anim.SetBool("facingright", facingRight);
		anim.SetBool("grounded", grounded);
	}

	void FixedUpdate()
	{
		if (life < 0)
			return ;
		
		float move;
		
		move = Input.GetAxis("Horizontal");

		timesinceouch += Time.deltaTime;
		invutime += Time.deltaTime;
		if (timesinceouch < ouchtime)
			return ;
		
		if (base.cannotmove == true)
			return ;

		Tapping(move);

		GroundCheck();

		Move(move);

		SlideCheck();

		anim.SetFloat("vely", rigidbody2D.velocity.y);

		rigidbody2D.velocity = new Vector2(move * maxSpeed, Mathf.Clamp(rigidbody2D.velocity.y, minYVelocity, maxYVelocity));
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
			}
			else if (timesincetapping > 0.2f)
				zonebam.gameObject.SetActive(false);
			else if (timesincetapping > 0.1f)
				zonebam.gameObject.SetActive(true);
		}
		if (!istapping && Input.GetKey(KeyCode.Mouse1))
		{
			istapping = true;
			timesincetapping = 0;
			anim.SetTrigger("istapping");
		}
	}

	void Move(float move)
	{
		if (grounded && audiosource.isPlaying == false && move != 0)
		{
			audiosource.loop = true;
			audiosource.clip = run;
			audiosource.Play();
		}
		else if (move == 0 && audiosource.clip == run)
			audiosource.Stop();

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
		anim.SetTrigger("death");
	}

	void ouch()
	{
		life--;
		if (life < 1)
			Die();
		else
			anim.SetTrigger("ouch");
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

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.collider.tag == "Slime")
		{
			if (rigidbody2D.velocity.y < slimeVelocityIgnore)
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
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
