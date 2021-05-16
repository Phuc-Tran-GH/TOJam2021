using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private GameObject beaver;
    [SerializeField] private float xOffset;
    [SerializeField] private float defaultY;
    [SerializeField] private float followBeaverHeight;

    // Start is called before the first frame update
    void Start()
    {
        ResetCamera();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = gameObject.transform.position;

        if (beaver.transform.position.x + xOffset > pos.x)
        {
            pos.x = beaver.transform.position.x + xOffset;
        }

        if (beaver.transform.position.y > followBeaverHeight)
        {
            pos.y = defaultY + beaver.transform.position.y - followBeaverHeight;
        }
        else
        {
            pos.y = defaultY;
        }

        gameObject.transform.position = pos;
    }

    public void ResetCamera()
    {
        Vector3 pos = gameObject.transform.position;
        pos.x = beaver.transform.position.x + xOffset;

        gameObject.transform.position = pos;
    }
}
