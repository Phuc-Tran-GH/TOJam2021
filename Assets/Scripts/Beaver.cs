using System.Collections;
using UnityEngine;

public class Beaver : MonoBehaviour
{
	[SerializeField] private Rigidbody2D rigidbody2D;
	[SerializeField] private GameObject biteCollider;
	[SerializeField] private Animator animator;
	[SerializeField] private AudioSource audio;
	[SerializeField] private Glider glider;
	[SerializeField] private Sprite[] tailSprites;
	[SerializeField] private SpriteRenderer tailRenderer;
	[SerializeField] private GameObject root;

	public float BiteDuration { get; private set; } = 0.2f;
	public float BiteCooldown { get; private set; } = 0.2f;

	public float GroundSoundCooldown { get; private set; } = 1f;

	public AudioClip launchSound;
	public AudioClip biteSound;
	public AudioClip deathSound;
	public AudioClip groundSound;
	public AudioClip jumpSound;

	private bool dead;
	private bool wasShot;
	private bool canBite = true;
	private bool canPlayGroundSound = true;
	private bool canSlap;

	private float defaultGravity = 0.9f;

    private void Start()
    {
		
    }

	public void Activate()
    {
	    root.SetActive(true);
    }

	public void Deactivate()
    {
		glider.gameObject.SetActive(false);
		root.SetActive(false);
    }

    public void ShootOutOfCannon(Vector2 direction)
	{
		audio.PlayOneShot(launchSound);
		SetDead(false);
		wasShot = true;
		transform.rotation = Quaternion.identity;
		rigidbody2D.gravityScale = defaultGravity;
		rigidbody2D.AddForce(direction * UpgradeManager.instance.GetCannonUpgradeMultiplier());
		rigidbody2D.AddTorque(-1);
		numSlaps = UpgradeManager.instance.GetSlapUpgradeLevel();
		Invoke(nameof(AllowSlap), 0.1f);

		glider.ResetGlider();
		if (UpgradeManager.instance.GetGliderUpgradeNum() > 0)
        {
			glider.SetGlider(UpgradeManager.instance.GetGliderUpgradeNum() - 1);
        }
		glider.gameObject.SetActive(false);
	}
	
	private void Update()
	{
		HandleInput();

		if (transform.position.y > 7.75f)
		{
			transform.position = new Vector3(transform.position.x, 7.75f, transform.position.z);
		}

		if (!dead && wasShot && UpgradeManager.instance.GetGliderUpgradeNum() > 0 && rigidbody2D.velocity.y < 0)
        {
			glider.gameObject.SetActive(true);
			glider.DeployGlider();

			rigidbody2D.gravityScale = defaultGravity * UpgradeManager.instance.GetGliderUpgradeMultiplier();
        }
	}

	private void HandleInput()
	{
		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
		{
			Bite();
		}
	}

	private void Bite()
	{
		if (wasShot && !dead && canBite && !biteCollider.activeSelf)
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
		
		if (other.gameObject.CompareTag("Ground"))
		{
			OnGroundCollision();
		}
	}

	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			OnGroundStay();
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
		animator.Play("BeaverImpact");
	}

	private void OnTreeBit()
	{
		audio.PlayOneShot(biteSound);
		animator.Play("BeaverBite");
	}

	private void OnGroundStay()
	{
		// After being shot from a cannon, restart the game once we're touching the ground with no velocity
		if (wasShot && rigidbody2D.velocity.SqrMagnitude() < 1f)
		{
			if (resetGameCoroutine == null)
			{
				resetGameCoroutine = StartCoroutine(ResetGameCoroutine());
			}
		}

		PlayGroundSound();
	}

	private int numSlaps = 0;
	private void OnGroundCollision()
	{
		if (wasShot && !dead && canSlap && numSlaps > 0)
		{
			audio.PlayOneShot(jumpSound);

			rigidbody2D.gravityScale = defaultGravity;
			glider.ResetGlider();
			glider.gameObject.SetActive(false);

			rigidbody2D.AddForce(new Vector2(400, 800));
			numSlaps--;
			animator.Play("BeaverJump");
		}

		if (dead)
		{
			animator.SetBool("deadGround", true);
		}
	}

	private Coroutine resetGameCoroutine;
	private IEnumerator ResetGameCoroutine()
	{
		yield return new WaitForSeconds(1); // wait a bit before restarting

		wasShot = false;
		canSlap = false;

		//open upgrade panel
		UpgradeManager.instance.OpenUpgradePanel();

		resetGameCoroutine = null;
		yield return null;
	}

	public void SetDead(bool isDead)
	{
		dead = isDead;
		animator.SetBool("dead", dead);

		if (!isDead)
		{
			rigidbody2D.velocity = Vector3.zero;
			animator.SetBool("deadGround", false);
		}
		
		if (wasShot && isDead && resetGameCoroutine == null)
		{
			GameManager.instance.CheckFurthestDistance();
			resetGameCoroutine = StartCoroutine(ResetGameCoroutine());
		}
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

	private void AllowSlap()
	{
		canSlap = true;
	}

	public void SetTail(int tail)
	{
		int index = Mathf.Clamp(tail, 0, tailSprites.Length - 1);
		tailRenderer.sprite = tailSprites[index];
	}
}
