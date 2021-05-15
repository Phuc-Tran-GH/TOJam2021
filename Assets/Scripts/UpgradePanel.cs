using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cannonCostText;
    [SerializeField] private TextMeshProUGUI gliderCostText;
    [SerializeField] private TextMeshProUGUI bounceCostText;

    private void Awake()
    {
        UpgradeManager.instance.CannonCostChangedEvent += CannonCostChangeHandler;
        UpgradeManager.instance.GliderCostChangedEvent += GliderCostChangeHandler;
        UpgradeManager.instance.SlapCostChangedEvent += BounceCostChangeHandler;
    }

    private void OnDestroy()
    {
        UpgradeManager.instance.CannonCostChangedEvent -= CannonCostChangeHandler;
        UpgradeManager.instance.GliderCostChangedEvent -= GliderCostChangeHandler;
        UpgradeManager.instance.SlapCostChangedEvent -= BounceCostChangeHandler;
    }


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CannonCostChangeHandler(int cost)
    {
        if (UpgradeManager.instance.HasCannonUpgrade())
        {
            cannonCostText.text = UpgradeManager.instance.GetCannonUpgradeCost().ToString();
        }
        else
        {
            cannonCostText.text = "SOLD OUT";
        }
    }

    private void GliderCostChangeHandler(int cost)
    {
        if (UpgradeManager.instance.HasGliderUpgrade())
        {
            gliderCostText.text = UpgradeManager.instance.GetGliderUpgradeCost().ToString();
        }
        else
        {
            gliderCostText.text = "SOLD OUT";
        }
    }

    private void BounceCostChangeHandler(int cost)
    {
        if (UpgradeManager.instance.HasSlapUpgrade())
        {
            bounceCostText.text = UpgradeManager.instance.GetSlapUpgradeCost().ToString();
        }
        else
        {
            bounceCostText.text = "SOLD OUT";
        }
    }
}
