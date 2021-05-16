using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DottedLine;

public class BounceUpgradeIconScript : MonoBehaviour
{
    public Sprite[] spriteArray;
    public Image image;
    public void UpgradeIcon(int upgradeNum){
    	Debug.Log("Sprite num: " + upgradeNum);
    	Debug.Log("Sprite array num: " + spriteArray.Length);

        Sprite newSprite = spriteArray[upgradeNum];
        image.sprite = newSprite;
    }

}
