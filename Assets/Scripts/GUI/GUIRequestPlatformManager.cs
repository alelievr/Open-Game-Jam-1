using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIRequestPlatformManager : MonoBehaviour {

	public Text		defaultPlatformText;
	public Image	defaultPlatformImage;
	public Text		icePlatformText;
	public Image	icePlatformImage;
	public Text		slimePlatformText;
	public Image	slimePlatformImage;

	public AudioClip	platformPlacedClip;

	[Space]
	public Color	disabledColor;
	public float	fadeOutDuration = .3f;

	string			defaultText;
	string			iceText;
	string			slimeText;

	Color			defaultColor;
	Color			iceColor;
	Color			slimeColor;
	
	PlatformRequestSystem	prs;
	AudioSource				audioSource;

	void OnEnable()
	{
		prs = Object.FindObjectOfType< PlatformRequestSystem >();

		prs.OnPlatformPlaced += OnPlatformPlaced;
		prs.OnPlatformRefill += OnPlatformRefill;
	}

	void OnDisable()
	{
		prs.OnPlatformPlaced -= OnPlatformPlaced;
		prs.OnPlatformRefill -= OnPlatformRefill;
	}
	
	void Start ()
	{
		defaultText = defaultPlatformText.text;
		defaultColor = defaultPlatformImage.color;
		iceText = icePlatformText.text;
		iceColor = icePlatformImage.color;
		slimeText = slimePlatformText.text;
		slimeColor = slimePlatformImage.color;

		audioSource = Camera.main.GetComponent< AudioSource >();

		foreach (var kp in prs.platforms)
		{
			UpdatePlatformText(kp.type, kp.availableNumber);
			if (kp.availableNumber == 0)
				DisablePlatformPreview(kp.type);
		}
	}

	public void OnPlatformPlaced(RequestPlatformType type, int remaining)
	{
		UpdatePlatformText(type, remaining);

		audioSource.PlayOneShot(platformPlacedClip, .4f);

		if (remaining == 0)
			DisablePlatformPreview(type);
	}

	public void OnPlatformRefill(RequestPlatformType type, int remaining)
	{
		UpdatePlatformText(type, remaining);

		EnablePlatformPreview(type);
	}

	void UpdatePlatformText(RequestPlatformType type, int remaining)
	{
		switch (type)
		{
			case RequestPlatformType.Default:
				defaultPlatformText.text = string.Format(defaultText, remaining);
				break ;
			case RequestPlatformType.Ice:
				icePlatformText.text = string.Format(iceText, remaining);
				break ;
			default:
				slimePlatformText.text = string.Format(slimeText, remaining);
				break ;
		}
	}
	
	void DisablePlatformPreview(RequestPlatformType type)
	{
		if (type == RequestPlatformType.Default)
			StartCoroutine(Utils.FadeOut(defaultPlatformImage, defaultColor, disabledColor, fadeOutDuration));
		if (type == RequestPlatformType.Ice)
			StartCoroutine(Utils.FadeOut(icePlatformImage, iceColor, disabledColor, fadeOutDuration));
		if (type == RequestPlatformType.Slime)
			StartCoroutine(Utils.FadeOut(slimePlatformImage, slimeColor, disabledColor, fadeOutDuration));
	}

	void EnablePlatformPreview(RequestPlatformType type)
	{
		if (type == RequestPlatformType.Default)
			StartCoroutine(Utils.FadeOut(defaultPlatformImage, disabledColor, defaultColor, fadeOutDuration));
		if (type == RequestPlatformType.Ice)
			StartCoroutine(Utils.FadeOut(icePlatformImage, disabledColor, iceColor, fadeOutDuration));
		if (type == RequestPlatformType.Slime)
			StartCoroutine(Utils.FadeOut(slimePlatformImage, disabledColor, slimeColor, fadeOutDuration));
	}
}
