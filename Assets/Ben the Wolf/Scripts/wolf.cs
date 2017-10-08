using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolf : MonoBehaviour {
	PlayerController	cible;
	bool				running;
	int 				dir = 0;
	int life = 2;
	public float		runspeed = 6f;
	public float		walkspeed = 2f;

	float 				maxspeed = 2;
	float				lastflip = 0;
	float				lastjump = 0;

	public float		distofjump = 10;
	public float		jumpspeed = 24;
	public float		jumpheigtspeed = 4;
	public float		preptimetojump = 0.5f;
	public float		jumprecovry = 0.5f;
	bool				isjumping = false;
	int					jumpstep = 0;
	float				distjumped = 0;
	float				timesincejump = 0;

	Animator		anim;
	new Rigidbody2D	rigidbody2D;
	[HideInInspector] public bool	facingRight = true;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		maxspeed = walkspeed;
		anim.SetFloat("speed", 0);
		Flip();
	}

	void jump()
	{
		if (jumpstep == 0)
		{
			distjumped = 0;
			jumpstep = 1;
			anim.SetBool("jump", true);
			timesincejump = 0;
			rigidbody2D.velocity = Vector2.zero;
		}
		else if (jumpstep == 1)
		{
			timesincejump += Time.deltaTime;
			if (timesincejump > preptimetojump)
				jumpstep = 2;
		}
		else if (jumpstep == 2)
		{
			rigidbody2D.velocity = new Vector2(dir * jumpspeed, jumpspeed);
			distjumped += jumpspeed;
			if (jumpspeed > distofjump)
			{
				jumpstep = 3;
				timesincejump = 0;
			}
		}
		else if (jumpstep == 3)
		{
			timesincejump += Time.deltaTime;
			if (timesincejump > jumprecovry)
			{
				jumpstep = 0;
				isjumping = false;
				anim.SetBool("jump", false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isjumping)
			jump();
		if (lastflip > 0)
			lastflip -= Time.deltaTime;
		if (cible && lastflip <= 0 && lastjump <= 0)
			dir = ((cible.transform.position - this.transform.position ).x < 0) ? -1 : 1;
		if (cible && lastflip <= 0 && Vector3.Distance(cible.transform.position, this.transform.position) < distofjump * 0.8)
			isjumping = true;
		if (lastflip <= 0 && dir > 0 && !facingRight)
			Flip();
		else if (lastflip <= 0 && dir < 0 && facingRight)
			Flip();
		rigidbody2D.velocity = new Vector2(dir * maxspeed, rigidbody2D.velocity.y);
	}

	void Flip()
	{
		lastflip = 0.5f;
		if (cible)
			dir = ((cible.transform.position - this.transform.position ).x < 0) ? -1 : 1;
		facingRight = !facingRight;
		transform.localScale = new Vector3((facingRight) ?	
			(transform.localScale.x > 0) ? transform.localScale.x : -transform.localScale.x :
			(transform.localScale.x > 0) ? -transform.localScale.x : transform.localScale.x,
		transform.localScale.y, transform.localScale.z);
	}

	void ouch()
	{
		life--;
		if (life == 0)
			GameObject.Destroy(this.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player")
		{
			anim.SetFloat("speed", 6);
			cible = other.GetComponent<PlayerController>();
			maxspeed = runspeed;
			dir = ((cible.transform.position - this.transform.position ).x < 0) ? -1 : 1;
		}
		if (other.tag == "Bam")
			this.ouch();
	}
}
