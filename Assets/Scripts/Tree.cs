using UnityEngine;

public class Tree : MonoBehaviour
{
	[SerializeField] private TreeTrunk trunk;

	private int poolNumber;

	public void UnBitten()
    {
		trunk.UnBitten();
	}

	public void SetPoolNumber(int p)
    {
		poolNumber = p;
    }

	public int GetPoolNumber()
    {
		return poolNumber;
    }
}
