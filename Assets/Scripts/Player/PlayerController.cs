using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Stopmoving {

	public float					maxSpeed = 1f;
	[HideInInspector] public bool	facingRight = false;

	[Space]
	public Transform	groundCheck;
	public LayerMask	groundMask;
	bool				grounded = false;
	public float		groundRadius = .2f;

	[Space]
	public float		jumpPower = 10f;
	public float		jumpIdle = .3f;
	bool				canJump = true;

	public int			life = 5;
	public float		timeInvuAfterOuch = 0.7f;
	public float		ouchtime = 0.2f;
	public float		ouchbacklash = 1f;
	float				timesinceouch = 100;
	float				invutime = 0;

	bool				istapping = false;
	public Collider2D	zonebam;
	float				timesincetapping;

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
		float move;

		timesinceouch += Time.deltaTime;
		invutime += Time.deltaTime;
		if (timesinceouch < ouchtime)
			return ;
		else
			anim.SetBool("ouch", false);
		if (base.cannotmove == true)
			return ;

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

		grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundMask);
		anim.SetBool("grounded", grounded);

		move = Input.GetAxis("Horizontal");

		if (grounded && audiosource.isPlaying == false && move != 0)
		{
			audiosource.loop = true;
			audiosource.clip = run;
			audiosource.Play();
		}
		else if (move == 0 && audiosource.clip == run)
			audiosource.Stop();

		rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);

		if (!istapping && move > 0 && !facingRight)
			Flip();
		else if (!istapping && move < 0 && facingRight)
			Flip();
		if (move != 0)
			anim.SetBool("moving", true);
		else
			anim.SetBool("moving", false);	

		anim.SetFloat("vely", rigidbody2D.velocity.y);
	}

	void Flip()
	{
		facingRight = !facingRight;
		anim.SetBool("facingright", facingRight);
		// if (!spriteRenderer)
		// {
		// 	eyesright.SetActive(!eyesright.activeInHierarchy);
		// 	eyesleft.SetActive(!eyesright.activeInHierarchy);
		// 	return;
		// }
		spriteRenderer.flipX = facingRight;
	}

	IEnumerator Die()
	{
		anim.SetBool("death", true);
		yield return new WaitForSeconds(1);
		GameObject.Destroy(this.gameObject);
			
	}

	void ouch()
	{
		life--;
		if (life < 1)
			StartCoroutine(Die());
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
		Gizmos.color = Color.green;
		Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
	}
}
