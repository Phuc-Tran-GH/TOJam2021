using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private int poolNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Beaver"))
        {
            OnCollected();
        }
    }

    private void OnCollected()
    {
        GameManager.instance.PowerUpCollected();
        gameObject.SetActive(false);
    }

    public void SetPoolNumber(int p)
    {
        poolNumber = p;
    }

    public int GetPoolNumer()
    {
        return poolNumber;
    }
}
