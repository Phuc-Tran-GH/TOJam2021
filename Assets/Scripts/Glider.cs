using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject beaver;
    [SerializeField] private Vector3 beaverOffset;

    public Sprite[] gliderSprites;

    public float deployTimer;
    public float deployBounceTimer;
    private float deployTimerCountdown;
    private float deployBounceTimerCountdown;
    private bool deployed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = beaver.transform.position + beaverOffset;
        if (deployed)
        {
            deployTimerCountdown -= Time.deltaTime;
            if (deployTimerCountdown <= 0)
            {
                deployTimerCountdown = 0;
                deployBounceTimerCountdown -= Time.deltaTime;
                if (deployBounceTimerCountdown <= - deployBounceTimer)
                {
                    deployBounceTimerCountdown = -deployBounceTimer;
                }
            }
            float gliderScale = 1.0f - (deployTimerCountdown / deployTimer) + 0.3f * (1.0f - Mathf.Abs(deployBounceTimerCountdown) / deployBounceTimer);

            spriteRenderer.gameObject.transform.localScale = new Vector3(gliderScale * 0.3f, gliderScale * 0.3f, 1.0f);
        }
    }

    public void SetGlider(int gliderNum)
    {
        spriteRenderer.sprite = gliderSprites[gliderNum];
    }

    public void DeployGlider()
    {
        if (!deployed)
        {
            deployed = true;
            deployTimerCountdown = deployTimer;
            deployBounceTimerCountdown = deployBounceTimer;
            spriteRenderer.gameObject.transform.localScale = new Vector3(0, 0, 1.0f);
        }
    }

    public void ResetGlider()
    {
        deployed = false;
    }
}
