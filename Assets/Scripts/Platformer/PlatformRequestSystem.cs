using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRequestSystem : MonoBehaviour {

	public enum RequestPlatformType
	{
		Default,
		Simle,
		Ice,
	}
	
	[System.Serializable]
	public class PlatformKey
	{
		public RequestPlatformType	type;
		public KeyCode				key;
		public GameObject			platformPrefab;
	}

	public List< PlatformKey >	platforms = new List< PlatformKey >();

	bool		showPreview = false;
	GameObject	previewGO;

	void Start()
	{
		
	}

	void Update()
	{
		foreach (var kp in platforms)
		{
			if (Input.GetKeyDown(kp.key))
			{
				previewGO = Instantiate(kp.platformPrefab);
				previewGO.GetComponent< Platform >().timeBeforeDispawn = 4f;
			}
		}

		if (showPreview && previewGO != null)
			previewGO.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		//place platform by pressing enter or return
		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
		{
			previewGO.GetComponent< Collider2D >().enabled = true;
			previewGO.GetComponent< ParticleSystem >().Play();
		}


		//remove preview by pressing escape
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Destroy(previewGO);
			showPreview = false;
		}
	}

}
