﻿using System;
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
	public int RunWood { get; private set; }
	public float FurthestDistance { get; private set; }
	public Beaver Beaver => beaver;

	public event Action<int> WoodChangedEvent;
	public event Action<int> RunWoodChangedEvent;

	private void Start(){
		powerBar = FindObjectOfType(typeof(PowerBar)) as PowerBar;
	}

	public void AddWood(int wood)
	{
		Wood += wood;
		RunWood += wood;
		WoodChangedEvent?.Invoke(Wood);
		RunWoodChangedEvent?.Invoke(Wood);
	}

	public void SpendWood(int wood)
    {
		Wood -= wood;
		WoodChangedEvent?.Invoke(Wood);
    }

	public void ResetBeaver()
	{
		cannon.shot = false;
		cannon.ResetCannon();
		beaver.transform.position = new Vector3(startPosition.position.x, startPosition.position.y + cannon.yOffsets[UpgradeManager.instance.GetCannonUpgradeNum()], startPosition.position.z);
		beaver.SetDead(false);
		beaver.Deactivate();
		camera.ResetCamera();
		levelGenerator.ClearLevel();
		
		powerBar.Activate();
		RunWood = 0;
		AddWood(0);
	}

	public void CheckFurthestDistance(){
		float beaverX = beaver.transform.position.x;
		if (beaverX > FurthestDistance) {
			FurthestDistance = beaverX;
		}
	}
}
