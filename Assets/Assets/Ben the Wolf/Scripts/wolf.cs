using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolf : MonoBehaviour {
	PlayerController	cible;
	bool				running;
	int 				dir = 0;
	public int 			life = 2;
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

	public bool			debugcamperspectiveactivated = false;
	public float		mapz;
	public GameObject	colider;
	public GameObject	ouchzone;

	Animator		anim;
	new Rigidbody2D	rigidbody2D;
	[HideInInspector] public bool	facingRight = true;

	// Use this for initialization
	void Start () {
		rigidbody2D = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		maxspeed = walkspeed;
		anim.SetFloat("speed", 0);
		if (debugcamperspectiveactivated)
			transform.position = new Vector3(transform.position.x, transform.position.y, mapz);
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
			rigidbody2D.velocity = Vector3.zero;
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
			if (distjumped > distofjump)
			{
				jumpstep = 3;
				timesincejump = 0;
				anim.SetBool("jump", false);
				// rigidbody2D.velocity = Vector3.zero;
			}
		}
		else if (jumpstep == 3)
		{
			timesincejump += Time.deltaTime;
			if (timesincejump > jumprecovry)
			{
				jumpstep = 0;
				isjumping = false;
			}
		}
	}
	
	void perspectivecorector()
	{
		Vector3 camdir;
		camdir = (this.transform.position - Camera.main.transform.position).normalized;
		transform.rotation = Quaternion.LookRotation(camdir);
		Debug.Log(transform.rotation);
		colider.transform.position = transform.position;
		ouchzone.transform.position = transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (debugcamperspectiveactivated)
			perspectivecorector();
		if (isjumping)
		{
			jump();
			return;
		}
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

	IEnumerator death()
	{
		anim.SetBool("death", true);
		yield return (new WaitForSeconds(1));
		GameObject.Destroy(this.gameObject);

	}

	IEnumerator ouchauxi()
	{
		rigidbody2D.velocity = new Vector3(dir * 10, 0, 0);
		yield return (new WaitForSeconds(1));
	}

	void ouch()
	{
		life--;
		if (life < 1)
			death();
		else
			ouchauxi();
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
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.tag == "bam")
		{
			Debug.Log("dfsaf");
			this.ouch();
		}
	}
}
