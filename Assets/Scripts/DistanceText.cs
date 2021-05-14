using TMPro;
using UnityEngine;

public class DistanceText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI distanceText;

	private Transform beaverTransform;

	private void Awake()
	{
		beaverTransform = GameManager.instance.Beaver.transform;
	}

	private void Update()
	{
		distanceText.text = $"{beaverTransform.position.x.ToString("F1")}M";
	}
}
