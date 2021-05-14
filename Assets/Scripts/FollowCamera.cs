using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private GameObject beaver;
    [SerializeField] private float xOffset;

    // Start is called before the first frame update
    void Start()
    {
        beaver = GameObject.Find("Beaver");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x = beaver.transform.position.x + xOffset;

        gameObject.transform.position = pos;
    }
}
