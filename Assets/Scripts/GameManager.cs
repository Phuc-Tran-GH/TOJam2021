using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] private Beaver beaver;
	
	public int Wood { get; private set; }
	public Beaver Beaver => beaver;

	public event Action<int> WoodChangedEvent;

	public void AddWood(int wood)
	{
		Wood += wood;
		WoodChangedEvent?.Invoke(Wood);
	}
}
