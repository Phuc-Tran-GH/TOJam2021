using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DottedLine;

public class CannonBehaviour : MonoBehaviour
{

    private LineRenderer lr;
    [SerializeField] Beaver beaver;
    public bool shot = false;
    private Vector2 direction;
    private float momentumX = 1200;
    private float momentumY = 200;
    [SerializeField] private PowerBar powerBar;

    [SerializeField] private GameObject[] cannonBarrels;
    [SerializeField] private GameObject[] cannonBases;
    [SerializeField] private Vector3 startingPosition;
    [SerializeField] private GameObject rootObject;

    public float[] yOffsets;

    // Start is called before the first frame update
    void Start()
    {
        /*
        lr = GetComponent<LineRenderer>();
        // Set some positions
        Vector3[] positions = new Vector3[2];
        positions[0] = transform.position;
        positions[1] = new Vector3(0.0f, -0.5f, 0.0f);
        
        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
        */
        ResetCannon();
        beaver.Deactivate();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        FaceMouse();

        if (Input.GetMouseButtonDown(0) && !shot) {
            float finalY = Mathf.Abs(mousePosition.y) - 100;
            int cursec = powerBar.cursec;
            momentumY = ((momentumX * (cursec / 100) / 5)) + finalY;
            beaver.Activate();
            beaver.ShootOutOfCannon(new Vector2(momentumX, momentumY));
            powerBar.Deactivate();
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();

            shot = true;
        }
    }

    void FaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        transform.right = direction;
        //RedrawLine(mousePosition.x, mousePosition.y);
    }

    void RedrawLine(float x, float y)
    {
        direction = new Vector2(
            x - lr.transform.position.x,
            y - lr.transform.position.y
        );

        lr.transform.right = direction;

        // Set some positions
        Vector3[] positions = new Vector3[2];
        positions[0] = new Vector3(transform.position.x, transform.position.y, 0.0f);
        positions[1] = new Vector3(x, y, 0.0f);

        lr.SetPositions(positions);

        float width = lr.startWidth;
        lr.material.mainTextureScale = new Vector2(Vector2.Distance(lr.GetPosition(0), lr.GetPosition(lr.positionCount - 1)) / lr.widthMultiplier, 1);


    }

    public void ResetCannon()
    {
        int upgradeNum = UpgradeManager.instance.GetCannonUpgradeNum();

        for (int i = 0; i < cannonBarrels.Length; i++)
        {
            if (i == upgradeNum)
            {
                cannonBarrels[i].SetActive(true);
                cannonBases[i].SetActive(true);
            }
            else
            {
                cannonBarrels[i].SetActive(false);
                cannonBases[i].SetActive(false);
            }
        }

        rootObject.transform.position = new Vector3(startingPosition.x, startingPosition.y + yOffsets[upgradeNum], startingPosition.z);
    }
}
