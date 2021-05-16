using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleCard : MonoBehaviour
{
	[SerializeField] private GameObject title;
	[SerializeField] private Button button;
	[SerializeField] private GameObject buttonGroup;

	[SerializeField] private GameObject mainDisplay;
	[SerializeField] private GameObject cannonBar;
	[SerializeField] private GameObject cannon;

	[SerializeField] private FollowCamera followCamera;
	[SerializeField] private CanvasGroup canvasGroup;

	[SerializeField] private GameObject tutorialPanel;

	[SerializeField] private AudioSource bounceSound;
	[SerializeField] private AudioSource startSound;

	private void Awake()
	{
		button.onClick.AddListener(ButtonHandler);
	}
	
	private void OnDestroy()
	{
		button.onClick.RemoveListener(ButtonHandler);
	}

	private void Start()
	{
		StartCoroutine(DoIntro());
	}

	private IEnumerator DoIntro()
	{
		mainDisplay.SetActive(false);
		cannonBar.SetActive(false);
		cannon.SetActive(false);
		
		title.transform.localPosition = new Vector3(-118, 969, 0);
		buttonGroup.transform.localPosition = new Vector3(561, 620, 0);
		
		// bounce title
		LeanTween.moveLocalY(title, 37, 1f).setEaseOutBounce();
		yield return new WaitForSeconds(0.2f);
		bounceSound.Play();
		
		yield return new WaitForSeconds(0.6f);

		// bounce play button
		LeanTween.moveLocalY(buttonGroup, -346.62f, 0.8f).setEaseOutBounce();
		yield return new WaitForSeconds(0.2f);
		bounceSound.Play();
	}

	private void ButtonHandler()
	{
		button.enabled = false;
		startSound.Play();
		StartCoroutine(StartGame());
	}

	private IEnumerator StartGame()
	{
		// bounce title
		LeanTween.alphaCanvas(canvasGroup, 0, 0.3f).setEaseInOutSine();

		yield return new WaitForSeconds(0.3f);

		tutorialPanel.SetActive(true);
		tutorialPanel.transform.localScale = Vector3.zero;
		LeanTween.scale(tutorialPanel, Vector3.one, 0.2f).setEaseOutBack();
		yield return new WaitUntil(() => tutorialPanel.activeSelf == false);
		
		// enable ui
		mainDisplay.SetActive(true);
		cannonBar.SetActive(true);
		cannon.SetActive(true);
		followCamera.enabled = true;
		
		GameManager.instance.DidStart = true;

		// wait for sound to finish then disable
		yield return new WaitForSeconds(0.2f);
		gameObject.SetActive(false);
		
	}
}
