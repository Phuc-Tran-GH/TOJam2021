using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using DottedLine;

public class CannonUpgradeIconScript : MonoBehaviour
{
    public Sprite[] spriteArray;
    public Image image;
    public void UpgradeIcon(int upgradeNum){
        Sprite newSprite = spriteArray[upgradeNum];
        image.sprite = newSprite;
    }

}
