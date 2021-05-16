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
    public void UpgradeIcon(int upgradeNum)
    {
        int index = Mathf.Min(upgradeNum, spriteArray.Length - 1);
        Sprite newSprite = spriteArray[index];
        image.sprite = newSprite;
    }

}
