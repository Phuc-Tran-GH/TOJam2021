using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
        lr.material = (Material) Resources.Load("Assets/BlackDotMaterial.mat");

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
    	FaceMouse();
        if (Input.GetMouseButtonDown(0) && !shot){
            Vector3 mousePosition = Input.mousePosition;
            beaver = FindObjectOfType(typeof(Beaver)) as Beaver; 
            float finalY = Mathf.Abs(mousePosition.y) - 100;
            //if (finalY < 0) finalY = 0;
            
            momentumY = (momentumX / 5) + finalY;
            beaver.ShootOutOfCannon(new Vector2(momentumX, momentumY));



            shot = true;
        }
        //RedrawLine();
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
    }


}
