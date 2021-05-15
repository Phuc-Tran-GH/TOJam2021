using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using DottedLine;

public class CannonBehaviour : MonoBehaviour
{
    
    private LineRenderer lr;
    public Beaver beaver;
    public bool shot = false;
    private Vector2 direction;
    private float momentumX = 1200;
    private float momentumY = 200;


    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
//        lr.material = (Material) Resources.Load("Assets/Images/BlackDotMaterial.mat");
        lr.material.SetTextureScale("_MainTex", new Vector2(0.5f, 0.5f));

        lr.textureMode =  LineTextureMode.Tile;
        // Set some positions
        Vector3[] positions = new Vector3[2];
        positions[0] = transform.position;
        positions[1] = new Vector3(0.0f, -0.5f, 0.0f);
        
        lr.positionCount = positions.Length;
        lr.SetPositions(positions);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
    	FaceMouse();

        healthbar healthbar = FindObjectOfType(typeof(healthbar)) as healthbar; 

        if (Input.GetMouseButtonDown(0) && !shot){
            beaver = FindObjectOfType(typeof(Beaver)) as Beaver; 
            float finalY = Mathf.Abs(mousePosition.y) - 100;
            int cursec = healthbar.cursec;
            momentumY = ((momentumX * (cursec / 100) / 5)) + finalY;
            beaver.ShootOutOfCannon(new Vector2(momentumX, momentumY));

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
        RedrawLine(mousePosition.x, mousePosition.y);
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

                float width =  lr.startWidth;
        lr.material.mainTextureScale = new Vector2(Vector2.Distance(lr.GetPosition(0), lr.GetPosition(lr.positionCount - 1)) / lr.widthMultiplier, 1);


    }


}
