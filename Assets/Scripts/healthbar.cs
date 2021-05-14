﻿using UnityEngine;
using UnityEngine.UI;

public class healthbar : MonoBehaviour
{
	public Image healthBarImage;
    public int cursec;
    private bool goingUp;
    private bool shot = false;
    private float momentumX = 1000;

    void Start()
    {
        cursec = 0;
        goingUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        printBar();
    }

    void printBar(){
        if (goingUp){
            cursec++;
        } else {
            cursec--;
        }

        if(cursec > 99){
            Debug.Log(cursec + " IS 100 MOFO");
            goingUp = false;
        } else if (cursec < 2) {
            goingUp = true;
            cursec++;
        }

        healthBarImage.fillAmount = Mathf.Clamp(cursec / 100f, 0, 1f);
    }
}