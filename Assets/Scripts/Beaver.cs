using System.Collections;
using UnityEngine;

public class Beaver : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private GameObject biteCollider;
	[SerializeField] private Animator animator;
	[SerializeField] private AudioSource audio;

	public float BiteDuration { get; private set; } = 0.2f;
	public float BiteCooldown { get; private set; } = 0.2f;

	public float GroundSoundCooldown { get; private set; } = 1f;

	public AudioClip launchSound;
	public AudioClip biteSound;
	public AudioClip deathSound;
	public AudioClip groundSound;

	private bool dead;
	private bool wasShot;
	private bool canBite = true;
	private bool canPlayGroundSound = true;

	public void Activate()
	{
		gameObject.SetActive(true);
	}


	public void Deactivate()
	{
		gameObject.SetActive(false);
	}

	public void ShootOutOfCannon(Vector2 direction)
	{
		audio.PlayOneShot(launchSound);
		SetDead(false);
		wasShot = true;
		transform.rotation = Quaternion.identity;
		rigidbody2D.AddForce(direction * UpgradeManager.instance.GetCannonUpgradeMultiplier());
		rigidbody2D.AddTorque(-1);
	}
	
	private void Update()
	{
		HandleInput();

		if (transform.position.y > 7.75f)
		{
			transform.position = new Vector3(transform.position.x, 7.75f, transform.position.z);
		}
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
		audio.PlayOneShot(deathSound);
		SetDead(true);
	}

	private void OnTreeBit()
	{
		audio.PlayOneShot(biteSound);
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

		PlayGroundSound();
	}

	private Coroutine resetGameCoroutine;
	private IEnumerator ResetGameCoroutine()
	{
		yield return new WaitForSeconds(1); // wait a bit before restarting

		wasShot = false;

		//open upgrade panel
		UpgradeManager.instance.OpenUpgradePanel();

		resetGameCoroutine = null;
		yield return null;
	}

	public void SetDead(bool isDead)
	{
		dead = isDead;
		animator.SetBool("dead", dead);
	}

	private void PlayGroundSound()
    {
		if (canPlayGroundSound)
		{
			audio.PlayOneShot(groundSound);
			canPlayGroundSound = false;
			Invoke(nameof(FinishPlayGroundSound), GroundSoundCooldown);
		}
	}

	private void FinishPlayGroundSound()
    {
		canPlayGroundSound = true;
    }
}
