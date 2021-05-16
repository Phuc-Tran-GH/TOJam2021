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

	[SerializeField] private float ceilingY;

	[SerializeField] private TrailRenderer launchTrail;
	[SerializeField] private TrailRenderer launchTrailGradient;
	[SerializeField] private TrailRenderer bounceTrail;
	[SerializeField] private TrailRenderer bounceTrailGradient;
	[SerializeField] private TrailRenderer dashTrail;
	[SerializeField] private TrailRenderer dashTrailFront;

	public float BiteDuration { get; private set; } = 0.2f;
	public float BiteCooldown { get; private set; } = 0.2f;

	public float GroundSoundCooldown { get; private set; } = 1f;

	public AudioClip launchSound;
	public AudioClip biteSound;
	public AudioClip deathSound;
	public AudioClip groundSound;
	public AudioClip jumpSound;
	public AudioClip powerUpCollect;

	private bool dead;
	private bool wasShot;
	private bool canBite = true;
	private bool canDash = true;
	private bool canPlayGroundSound = true;
	private bool canSlap;
	private bool bonusSlap = false;

	private float defaultGravity = 0.9f;
	private float dashForce = 2000f;

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
		numSlaps = 0;
		Invoke(nameof(AllowSlap), 0.1f);

		glider.ResetGlider();
		if (UpgradeManager.instance.GetGliderUpgradeNum() > 0)
        {
			glider.SetGlider(UpgradeManager.instance.GetGliderUpgradeNum() - 1);
        }
		glider.gameObject.SetActive(false);

		launchTrail.emitting = true;
		launchTrailGradient.emitting = true;
		bounceTrail.emitting = false;
		bounceTrailGradient.emitting = false;
		dashTrail.emitting = false;
		dashTrailFront.emitting = false;
	}
	
	private void Update()
	{
		HandleInput();

		if (transform.position.y > ceilingY)
		{
			transform.position = new Vector3(transform.position.x, ceilingY, transform.position.z);
		}

		/*if (rigidbody2D.velocity.y < 0)
        {
			//stop trails
			launchTrail.emitting = false;
			launchTrailGradient.emitting = false;
			bounceTrail.emitting = false;
			bounceTrailGradient.emitting = false;
		}*/

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
		else if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftControl))
        {
			DownDash();
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

	private void DownDash()
    {
		if (wasShot && !dead && canDash)
		{
			Vector2 direction = new Vector2(0, -dashForce); 
			transform.rotation = Quaternion.identity;
			rigidbody2D.gravityScale = defaultGravity;
			rigidbody2D.AddForce(direction * UpgradeManager.instance.GetCannonUpgradeMultiplier());
			rigidbody2D.AddTorque(-1);
			canDash = false;

			launchTrail.emitting = false;
			launchTrailGradient.emitting = false;
			bounceTrail.emitting = false;
			bounceTrailGradient.emitting = false;

			dashTrail.emitting = true;
			dashTrailFront.emitting = true;
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
		if (!dead)
		{
			audio.PlayOneShot(deathSound);
			SetDead(true);
			animator.Play("BeaverImpact");
		}
	}

	private void OnTreeBit()
	{
		audio.PlayOneShot(biteSound);
		animator.Play("BeaverBite");
	}

	private void OnGroundStay()
	{
		// After being shot from a cannon, restart the game once we're touching the ground with no velocity
		bool isStopped = rigidbody2D.velocity.SqrMagnitude() < 1f;
		if (wasShot && isStopped)
		{
			if (resetGameCoroutine == null)
			{
				GameManager.instance.CheckFurthestDistance();
				resetGameCoroutine = StartCoroutine(ResetGameCoroutine());
			}
		}
	}

	private int numSlaps = 0;
	private void OnGroundCollision()
	{
		if (wasShot && !dead && !canDash && canSlap)
		{
			audio.PlayOneShot(jumpSound);

			rigidbody2D.gravityScale = defaultGravity;
			rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
			glider.ResetGlider();
			glider.gameObject.SetActive(false);

			rigidbody2D.AddForce(new Vector2(1000, 800) * (UpgradeManager.instance.GetSlapUpgradeMultiplier() * Mathf.Pow(0.9f, numSlaps)));

			dashTrail.emitting = false;
			dashTrailFront.emitting = false;
			bounceTrail.emitting = true;
			bounceTrailGradient.emitting = true;

			numSlaps++;
			if (bonusSlap)
            {
				canDash = true;
				bonusSlap = false;
            }
			else
			{
				canSlap = false;
			}
			animator.Play("BeaverJump");
		}

		if (dead)
		{
			animator.SetBool("deadGround", true);
		}
		
		PlayGroundSound();
	}

	private Coroutine resetGameCoroutine;
	private IEnumerator ResetGameCoroutine()
	{
		yield return new WaitForSeconds(1); // wait a bit before restarting

		wasShot = false;
		canSlap = false;
		canDash = false;

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

		launchTrail.emitting = false;
		launchTrailGradient.emitting = false;
		bounceTrail.emitting = false;
		bounceTrailGradient.emitting = false;
		dashTrail.emitting = false;
		dashTrailFront.emitting = false;
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

	public void AllowSlap()
	{
		canSlap = true;
		canDash = true;
		bonusSlap = false;
	}

	public void SetTail(int tail)
	{
		int index = Mathf.Clamp(tail, 0, tailSprites.Length - 1);
		tailRenderer.sprite = tailSprites[index];
	}

	public void RefreshSlaps()
    {
		numSlaps = 0;

		audio.PlayOneShot(powerUpCollect);

		if (!canSlap)
        {
			AllowSlap();
        }

		bonusSlap = true;

	}
}
