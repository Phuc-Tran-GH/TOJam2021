using UnityEngine;

public class Tree : MonoBehaviour
{
	[SerializeField] private TreeTrunk trunk;

	public void UnBitten()
    {
		trunk.UnBitten();
	}
}
