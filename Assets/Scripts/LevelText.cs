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
    	cannonLevel.text = $"Lv {upgradeManager.GetCannonUpgradeNum() + 1}";
    	gliderLevel.text = $"Lv {upgradeManager.GetGliderUpgradeNum() + 1}";
    	bounceLevel.text = $"Lv {upgradeManager.GetSlapUpgradeNum() + 1}";
    }

}
