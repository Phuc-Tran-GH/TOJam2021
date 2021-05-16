using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPool : MonoBehaviour
{
    public PowerUp[] powerupPrefabs;

    private List<PowerUp>[] powerupPools;

    // Start is called before the first frame update
    void Start()
    {
        powerupPools = new List<PowerUp>[powerupPrefabs.Length];

        for (int i = 0; i < powerupPools.Length; i++)
        {
            powerupPools[i] = new List<PowerUp>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public PowerUp GetPowerUp(int index)
    {
        PowerUp powerUp;
        if (powerupPools[index].Count == 0)
        {
            powerUp = Instantiate(powerupPrefabs[index], new Vector3(0, 0, 1), Quaternion.identity);
            powerUp.SetPoolNumber(index);
        }
        else
        {
            powerUp = powerupPools[index][0];
            powerUp.gameObject.SetActive(true);
            powerupPools[index].Remove(powerUp);
        }

        return powerUp;
    }

    public void ReplacePowerUp(PowerUp powerUp)
    {
        powerUp.gameObject.SetActive(false);
        powerupPools[powerUp.GetPoolNumer()].Add(powerUp);
    }

    public int GetNumPowerUpTypes()
    {
        return powerupPrefabs.Length;
    }
}
