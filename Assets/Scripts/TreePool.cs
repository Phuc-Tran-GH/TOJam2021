using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePool : MonoBehaviour
{
    public Tree[] treePrefabs;

    private List<Tree>[] treePools;

    // Start is called before the first frame update
    void Start()
    {
        treePools = new List<Tree>[treePrefabs.Length];

        for (int i = 0; i < treePools.Length; i++)
        {
            treePools[i] = new List<Tree>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Tree GetTree(int index)
    {
        Tree tree;
        if (treePools[index].Count == 0)
        {
            tree = Instantiate(treePrefabs[index], new Vector3(0, 0, 1), Quaternion.identity);
            tree.SetPoolNumber(index);
        }
        else
        {
            tree = treePools[index][0];
            tree.gameObject.SetActive(true);
            treePools[index].Remove(tree);
        }

        return tree;
    }

    public void ReplaceTree(Tree tree)
    {
        tree.gameObject.SetActive(false);
        tree.UnBitten();
        treePools[tree.GetPoolNumber()].Add(tree);
    }

    public int GetNumTreeTypes()
    {
        return treePrefabs.Length;
    }
}
