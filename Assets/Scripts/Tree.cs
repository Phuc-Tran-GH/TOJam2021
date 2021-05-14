using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tree : MonoBehaviour
{
	[SerializeField] private BoxCollider2D collider;
	[SerializeField] private GameObject biteMarks;
	[SerializeField] private TextMeshPro pointsText;
	[SerializeField] private int points = 1;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bite"))
		{
			OnBitten(other.transform.position);
		}
	}

	private void OnBitten(Vector3 bitePosition)
	{
		// Disable collider, allowing beaver to pass through
		collider.enabled = false;
		
		// Show bite marks
		biteMarks.transform.position = new Vector3(transform.position.x, bitePosition.y, 0);
		biteMarks.SetActive(true);
		
		// Show points text
		pointsText.transform.position = new Vector3(pointsText.transform.position.x, bitePosition.y, 0);
		pointsText.text = $"+{points}";
		pointsText.transform.localScale = Vector3.zero;
		
		// Scale up then down
		var seq = LeanTween.sequence();
		seq.append(LeanTween.scale(pointsText.gameObject, new Vector3(1.2f, 1.2f, 1f), 0.3f).setEaseOutBack());
		seq.append(LeanTween.scale(pointsText.gameObject, Vector3.zero, 0.6f));
	}

	public void UnBitten()
    {
		// Reenable collider
		collider.enabled = true;

		// Hide bite marks
		biteMarks.SetActive(false);
	}
}
