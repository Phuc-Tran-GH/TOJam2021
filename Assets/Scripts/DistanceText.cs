using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DistanceText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI distanceText;
	[SerializeField] private Transform beaverTransform;

	private void Update()
	{
		distanceText.text = $"{beaverTransform.position.x.ToString("F1")}M";
	}
}
