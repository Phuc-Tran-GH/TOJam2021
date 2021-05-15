using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI cannonLevel;
	[SerializeField] private TextMeshProUGUI gliderLevel;
	[SerializeField] private TextMeshProUGUI bounceLevel;
	private UpgradeManager upgradeManager;

    // Start is called before the first frame update
    void Start()
    {
        upgradeManager =  UpgradeManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        Show();
    }


    void Show(){
    	Debug.Log("Show!!!!");
    	Debug.Log($"{upgradeManager.GetCannonUpgradeNum()}");
    	cannonLevel.text = $"{upgradeManager.GetCannonUpgradeNum()}";
    	gliderLevel.text = $"{upgradeManager.GetGliderUpgradeNum()}";
    	bounceLevel.text = $"{upgradeManager.GetSlapUpgradeNum()}";
    }

}
