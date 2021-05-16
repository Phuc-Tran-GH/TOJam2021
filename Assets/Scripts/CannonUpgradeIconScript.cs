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
    public void UpgradeIcon(int upgradeNum)
    {
        int index = Mathf.Clamp(upgradeNum - 1, 0, spriteArray.Length - 1);
        Sprite newSprite = spriteArray[index];
        image.sprite = newSprite;
        
        var newColor = image.color;
        newColor.a = upgradeNum == 0 ? 0.3f : 1f;
        image.color = newColor;
    }

}
