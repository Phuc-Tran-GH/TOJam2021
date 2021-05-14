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

    private GameObject beaver;

    private List<Tree> trees;

    // Start is called before the first frame update
    void Start()
    {
        beaver = GameObject.Find("Beaver");
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
                treePool.ReplaceTree(0, trees[0]);
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
            Tree tree = treePool.GetTree(0);
            Vector3 treePosition = levelStartLocation;

            lastX = lastX + Random.Range(minTreeSpacing, maxTreeSpacing);

            treePosition.x = lastX;

            tree.transform.position = treePosition;
            trees.Add(tree);           
        }
    }

    private void ClearLevel()
    {
        while(trees.Count > 0)
        {
            treePool.ReplaceTree(0, trees[0]);
            trees.Remove(trees[0]);
        }
    }
}
