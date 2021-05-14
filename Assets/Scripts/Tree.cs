using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
	[SerializeField] private BoxCollider2D collider;
	[SerializeField] private GameObject biteMarks;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Bite"))
		{
			OnBitten(other.transform.position);
		}
	}

	private void OnBitten(Vector3 bitePosition)
	{
		collider.enabled = false;
		biteMarks.transform.position = new Vector3(transform.position.x, bitePosition.y, 0);
		biteMarks.SetActive(true);
	}
}
