using System;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] private Beaver beaver;
	[SerializeField] private CannonBehaviour cannon;
	[SerializeField] private PowerBar powerBar;
	[SerializeField] private LevelGenerator levelGenerator;
	[SerializeField] private Transform startPosition;
	[SerializeField] private FollowCamera camera;

	public int Wood { get; private set; }
	public Beaver Beaver => beaver;

	public event Action<int> WoodChangedEvent;

	private void Start(){
		powerBar = FindObjectOfType(typeof(PowerBar)) as PowerBar;
	}

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
		beaver.Deactivate();
		camera.ResetCamera();
		levelGenerator.ClearLevel();

		powerBar.Activate();

	}
}
