using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum RequestPlatformType
{
	Default,
	Slime,
	Ice,
}

public class PlatformRequestSystem : MonoBehaviour {
	
	[System.Serializable]
	public class PlatformKey
	{
		public RequestPlatformType	type;
		public KeyCode				key;
		public int					availableNumber = 1;
		public float				timeBeforeDispawn = 4;
		public GameObject			platformPrefab;
		public GameObject			platformMarkPrefab;
		public GameObject			PsPrefab;
	}

	public PlatformKey	defaultPlatform;
	public PlatformKey	icePlatform;
	public PlatformKey	slimePlatform;

	public IEnumerable< PlatformKey > platforms {
		get {
			yield return defaultPlatform;
			yield return icePlatform;
			yield return slimePlatform;
		}
	}

	public event Action< RequestPlatformType, int >		OnPlatformPlaced;
	public event Action< RequestPlatformType, int >		OnPlatformRefill;

	bool						showPreview = false;
	GameObject					previewGO;
	new Camera					camera;
	PlatformKey					currentPlatformKey;

	void Start()
	{
		camera = Camera.main;
	}

	IEnumerator DelayPlatformSpawn(Transform psChild)
	{
		yield return new WaitForSeconds(0.2f);
		psChild.gameObject.SetActive(true);
		Platform pf = psChild.gameObject.GetComponent< Platform >();
		pf.Init(currentPlatformKey.timeBeforeDispawn);
		ParticleSystem	ps = psChild.gameObject.GetComponent< ParticleSystem >();
		ps.Play();
		currentPlatformKey.availableNumber--;
		OnPlatformPlaced(currentPlatformKey.type, currentPlatformKey.availableNumber);
		StartCoroutine(RefillPlatform(currentPlatformKey));
		showPreview = false;
	}

	void Update()
	{
		foreach (var kp in platforms)
		{
			if (Input.GetKeyDown(kp.key) && kp.availableNumber > 0)
			{
				// destroy last object
				if (showPreview && previewGO != null)
					Destroy(previewGO);
			
				previewGO = Instantiate(kp.platformMarkPrefab);
				// Platform p = previewGO.GetComponent< Platform >();
				// previewGO.GetComponent< Collider2D >().enabled = false;
				currentPlatformKey = kp;
				showPreview = true;
			}
		}

		if (showPreview && previewGO != null)
		{
			Vector3 p = camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -camera.transform.position.z));
			p.z = 0;
			previewGO.transform.position = p;
			
			//place platform by pressing enter, return or mouse left click
			if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetMouseButtonDown(0))
			{

				GameObject psRef = Instantiate(currentPlatformKey.PsPrefab);
				Transform psChild = psRef.transform.GetChild(0);
				psRef.transform.position = (p + (Vector3.up * 10.2f));
				Destroy(previewGO);
				StartCoroutine(DelayPlatformSpawn(psChild));
				// previewGO.GetComponent< Collider2D >().enabled = true;
				// Platform pf = previewGO.GetComponent< Platform >();
				// pf.Init(currentPlatformKey.timeBeforeDispawn);
				// ParticleSystem	ps = previewGO.GetComponent< ParticleSystem >();
				// ps.Simulate(2f);
				// ps.Play();
				// currentPlatformKey.availableNumber--;
				// OnPlatformPlaced(currentPlatformKey.type, currentPlatformKey.availableNumber);
				// StartCoroutine(RefillPlatform(currentPlatformKey));
				// showPreview = false;
			}
		}

		//remove preview by pressing escape
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Destroy(previewGO);
			showPreview = false;
		}
	}

	IEnumerator	RefillPlatform(PlatformKey p)
	{
		yield return new WaitForSeconds(p.timeBeforeDispawn);
		p.availableNumber++;
		OnPlatformRefill(p.type, p.availableNumber);
		
	}

}
