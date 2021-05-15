using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject beaver;
    [SerializeField] private float xOffset;

    // Start is called before the first frame update
    void Start()
    {
        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x = beaver.transform.position.x + xOffset;

        if (pos.x > gameObject.transform.position.x)
        {
            gameObject.transform.position = pos;
        }
    }

    public void ResetCamera()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x = beaver.transform.position.x + xOffset;

        gameObject.transform.position = pos;
    }
}
