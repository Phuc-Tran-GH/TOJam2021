using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public TreePool treePool;
    public PowerUpPool powerupPool;

    public float minTreeSpacing;
    public float maxTreeSpacing;
    public float cullDistance;
    public Vector3 levelStartLocation;
    public Vector3 powerUpStartLocation;
    public float biomeLength;

    [SerializeField] private GameObject beaver;

    private List<Tree> trees;
    private List<PowerUp> powerUps;

    // Start is called before the first frame update
    void Start()
    {
        trees = new List<Tree>();
        powerUps = new List<PowerUp>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        float minX = beaver.transform.position.x - cullDistance;
        float maxX = beaver.transform.position.x + cullDistance;

        //cull old trees
        while (trees.Count > 0)
        {
            if (trees[0].transform.position.x < minX)
            {
                int biomeNumber = Mathf.Min((int)((trees[0].transform.position.x - levelStartLocation.x) / biomeLength), treePool.GetNumTreeTypes());
                treePool.ReplaceTree(trees[0]);
                trees.Remove(trees[0]);
            }
            else
            {
                break;
            }
        }

        //cull old powerups
        while (powerUps.Count > 0)
        {
            if (powerUps[0].transform.position.x < minX)
            {
                powerupPool.ReplacePowerUp(powerUps[0]);
                powerUps.Remove(powerUps[0]);
            }
            else
            {
                break;
            }
        }

        float lastX = Mathf.Max(minX, levelStartLocation.x);

        //create new trees
        if (trees.Count > 0)
        {
            lastX = trees[trees.Count - 1].transform.position.x;
        }

        while (lastX < maxX)
        {
            Vector3 treePosition = levelStartLocation;
            Vector3 powerUpPosition = powerUpStartLocation;

            float previousX = lastX;
            lastX = lastX + Random.Range(minTreeSpacing, maxTreeSpacing);

            //randomly spawn a power up before the tree
            if (previousX > powerUpStartLocation.x && Random.Range(0, 4) == 0)
            {
                int randomPowerUp = Random.Range(0, powerupPool.GetNumPowerUpTypes());

                PowerUp powerUp = powerupPool.GetPowerUp(randomPowerUp);

                powerUpPosition.x = (previousX + lastX) / 2;

                powerUp.transform.position = powerUpPosition;
                powerUps.Add(powerUp);
            }


            int biomeNumber = (int)((lastX - levelStartLocation.x) / biomeLength) % treePool.GetNumTreeTypes();

            Tree tree = treePool.GetTree(biomeNumber);

            treePosition.x = lastX;

            tree.transform.position = treePosition;
            trees.Add(tree);           
        }
    }

    public void ClearLevel()
    {
        while(trees.Count > 0)
        {
            int biomeNumber = (int)((trees[0].transform.position.x - levelStartLocation.x) / biomeLength) % treePool.GetNumTreeTypes();
            treePool.ReplaceTree(trees[0]);
            trees.Remove(trees[0]);
        }

        while(powerUps.Count > 0)
        {
            powerupPool.ReplacePowerUp(powerUps[0]);
            powerUps.Remove(powerUps[0]);
        }
    }
}
