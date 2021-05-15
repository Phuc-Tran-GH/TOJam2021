using TMPro;
using UnityEngine;

public class RunWoodText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI WoodText;

	private void Awake()
	{
		GameManager.instance.RunWoodChangedEvent += RunWoodChangedEventHandler;
	}

	private void OnDestroy()
	{
		GameManager.instance.RunWoodChangedEvent -= RunWoodChangedEventHandler;
	}

	private void Start()
	{
		UpdateText();
	}

	private void RunWoodChangedEventHandler(int wood)
	{
		UpdateText();
	}

	private void UpdateText()
	{
		WoodText.text = $"{GameManager.instance.RunWood}";
	}
}
