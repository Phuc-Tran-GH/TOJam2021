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
		var dist = Mathf.Max(0, beaverTransform.position.x);
		distanceText.text = $"{dist.ToString("F1")}M";
	}
}
