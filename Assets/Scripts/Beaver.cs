using UnityEngine;

public class Beaver : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private GameObject biteCollider;

	[SerializeField] private Animator animator;

	public float BiteDuration { get; private set; } = 0.2f;
	public float BiteCooldown { get; private set; } = 0.2f;

	private bool dead;

	public void ShootOutOfCannon(Vector2 direction)
	{
		dead = false;
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
		if (!dead && !biteCollider.activeSelf)
		{
			// Enable bite collider
			biteCollider.SetActive(true);
			
			// Show biting sprite
			animator.SetBool("biting", true);

			Invoke(nameof(FinishBite), BiteDuration);
		}
	}

	private void FinishBite()
	{
		CancelInvoke(nameof(FinishBite));
		biteCollider.SetActive(false);
		animator.SetBool("biting", false);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Tree"))
		{
			OnTreeCollision();
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
		dead = true;
		animator.SetBool("dead", true);
	}

	private void OnTreeBit()
	{
		animator.Play("BeaverBite");
	}
}
