using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimerArrow : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    public float animationTimer;
    private float animationTimerCountdown;
    private float startingX;

    // Start is called before the first frame update
    void Start()
    {
        startingX = transform.localPosition.x;
    }

    // Update is called once per frame
    void Update()
    {
        animationTimerCountdown += Time.deltaTime;

        if (animationTimerCountdown > animationTimer )
        {
            animationTimerCountdown = 0;
        }

        Vector3 pos = transform.localPosition;
        pos.x = startingX + (animationTimerCountdown / animationTimer);
        transform.localPosition = pos;

    }
}
