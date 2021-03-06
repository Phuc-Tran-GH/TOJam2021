using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGreyScript : MonoBehaviour
{

	public bool isCannon = false;
	public bool isGlider = false;
	public bool isBounce = false;

	public Image image;
	public Color32 originalColor;
	public Color32 tintColor = new Color32(255,255,225,100);

    // Start is called before the first frame update
    void Start()
    {
        originalColor = image.color;
    }

    // Update is called once per frame
    void Update()
    {
    	bool result = false;
    	if(isCannon){
            UpgradeManager.instance.GetCannonUpgradeCost();
        	result = GameManager.instance.Wood < UpgradeManager.instance.GetCannonUpgradeCost() || !UpgradeManager.instance.HasCannonUpgrade();
        } else if (isGlider){
        	result = GameManager.instance.Wood < UpgradeManager.instance.GetGliderUpgradeCost() || !UpgradeManager.instance.HasGliderUpgrade();
        } else if (isBounce){
        	result = GameManager.instance.Wood < UpgradeManager.instance.GetSlapUpgradeCost() || !UpgradeManager.instance.HasSlapUpgrade();
        }

        if (result){
        	image.color = tintColor;
        } else {
        	image.color = originalColor;
        }
    }
}
