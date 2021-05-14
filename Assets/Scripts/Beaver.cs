using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beaver : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private GameObject biteCollider;

	public float BiteDuration { get; private set; } = 0.2f;
	public float BiteCooldown { get; private set; } = 0.2f;


	private void Start()
	{

	}

	public void ShootOutOfCannon(Vector2 direction){
		rigidbody2D.AddForce(direction);
		rigidbody2D.AddTorque(-1);
	}
	
	private void Update()
	{
		HandleInput();
	}

	private void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Bite();
		}
	}

	private void Bite()
	{
		if (!biteCollider.activeSelf)
		{
			biteCollider.SetActive(true);
			Invoke(nameof(FinishBite), BiteDuration);
		}
	}

	private void FinishBite()
	{
		CancelInvoke(nameof(FinishBite));
		biteCollider.SetActive(false);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Tree"))
		{
			OnTreeCollision();
		}
	}

	private void OnTreeCollision()
	{
		// TODO
	}
}
