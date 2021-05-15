using System;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] private Beaver beaver;
	[SerializeField] private CannonBehaviour cannon;
	[SerializeField] private LevelGenerator levelGenerator;
	[SerializeField] private Transform startPosition;
	
	public int Wood { get; private set; }
	public Beaver Beaver => beaver;

	public event Action<int> WoodChangedEvent;

	public void AddWood(int wood)
	{
		Wood += wood;
		WoodChangedEvent?.Invoke(Wood);
	}

	public void SpendWood(int wood)
    {
		Wood -= wood;
		WoodChangedEvent?.Invoke(Wood);
    }

	public void ResetBeaver()
	{
		cannon.shot = false;
		beaver.transform.position = startPosition.position;
		beaver.SetDead(false);
		levelGenerator.ClearLevel();
	}
}
