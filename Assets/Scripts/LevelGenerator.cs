using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public TreePool treePool;

    public float minTreeSpacing;
    public float maxTreeSpacing;
    public float cullDistance;
    public Vector3 levelStartLocation;
    public float biomeLength;

    [SerializeField] private GameObject beaver;

    private List<Tree> trees;

    // Start is called before the first frame update
    void Start()
    {
        trees = new List<Tree>();
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
                int biomeNumber = (int)((trees[0].transform.position.x - levelStartLocation.x) / biomeLength) % treePool.GetNumTreeTypes();
                treePool.ReplaceTree(biomeNumber, trees[0]);
                trees.Remove(trees[0]);
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

            lastX = lastX + Random.Range(minTreeSpacing, maxTreeSpacing);

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
            treePool.ReplaceTree(biomeNumber, trees[0]);
            trees.Remove(trees[0]);
        }
    }
}
