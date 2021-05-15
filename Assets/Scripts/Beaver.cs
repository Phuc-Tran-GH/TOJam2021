using System.Collections;
using UnityEngine;

public class Beaver : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private GameObject biteCollider;
	[SerializeField] private Animator animator;

	public float BiteDuration { get; private set; } = 0.2f;
	public float BiteCooldown { get; private set; } = 0.2f;

	private bool dead;
	private bool wasShot;
	private bool canBite = true;

	public void ShootOutOfCannon(Vector2 direction)
	{
		SetDead(false);
		wasShot = true;
		transform.rotation = Quaternion.identity;
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
		if (!dead && canBite && !biteCollider.activeSelf)
		{
			// Enable bite collider
			biteCollider.SetActive(true);
			
			// Show biting sprite
			animator.SetBool("biting", true);

			canBite = false;

			Invoke(nameof(FinishBite), BiteDuration);
		}
	}

	private void FinishBite()
	{
		CancelInvoke(nameof(FinishBite));
		biteCollider.SetActive(false);
		animator.SetBool("biting", false);
		Invoke(nameof(FinishCooldown), BiteCooldown);
	}

	private void FinishCooldown()
	{
		canBite = true;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Tree"))
		{
			OnTreeCollision();
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			OnGroundCollision();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Tree"))
		{
			OnTreeBit();
		}
	}

	private void OnTreeCollision()
	{
		SetDead(true);
	}

	private void OnTreeBit()
	{
		animator.Play("BeaverBite");
	}

	private void OnGroundCollision()
	{
		// After being shot from a cannon, restart the game once we're touching the ground with no velocity
		if (wasShot && rigidbody2D.velocity.SqrMagnitude() < 0.1f)
		{
			if (resetGameCoroutine == null)
			{
				resetGameCoroutine = StartCoroutine(ResetGameCoroutine());
			}
		}
	}

	private Coroutine resetGameCoroutine;
	private IEnumerator ResetGameCoroutine()
	{
		yield return new WaitForSeconds(1); // wait a bit before restarting
		wasShot = false;
		SetDead(false);

		//open upgrade panel
		UpgradeManager.instance.OpenUpgradePanel();

		resetGameCoroutine = null;
		yield return null;
	}

	private void SetDead(bool isDead)
	{
		dead = isDead;
		animator.SetBool("dead", dead);
	}
}
