using TMPro;
using UnityEngine;

public class WoodText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI woodText;

	private void Awake()
	{
		GameManager.instance.WoodChangedEvent += WoodChangedEventHandler;
	}

	private void OnDestroy()
	{
		GameManager.instance.WoodChangedEvent -= WoodChangedEventHandler;
	}

	private void Start()
	{
		UpdateText();
	}

	private void WoodChangedEventHandler(int wood)
	{
		UpdateText();
	}

	private void UpdateText()
	{
		woodText.text = $"{GameManager.instance.Wood} Wood";
	}
}
