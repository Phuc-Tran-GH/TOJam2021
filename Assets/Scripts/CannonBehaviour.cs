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
    public float cannonPower = 12000.0f;

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
        
        FaceMouse();

        if (Input.GetMouseButtonDown(0) && !shot) {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (mousePosition.x < transform.position.x + 4.25f)
            {
                mousePosition.x = transform.position.x + 4.25f;
            }

            if (mousePosition.y < transform.position.y + 4.25f)
            {
                mousePosition.y = transform.position.y + 4.25f;
            }

            float powerBarPercent = powerBar.GetPowerBarPercent();
            Vector2 direction = new Vector2(
                mousePosition.x - transform.position.x,
                mousePosition.y - transform.position.y
            );
            Vector2 power = direction.normalized * cannonPower * (0.5f + 0.5f * powerBarPercent);

            beaver.Activate();
            beaver.ShootOutOfCannon(new Vector2(power.x, power.y));
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

        if (mousePosition.x < transform.position.x + 4.25f)
        {
            mousePosition.x = transform.position.x + 4.25f;
        }

        if (mousePosition.y < transform.position.y + 4.25f)
        {
            mousePosition.y = transform.position.y + 4.25f;
        }

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
