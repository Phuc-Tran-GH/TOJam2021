using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cannonCostText;
    [SerializeField] private TextMeshProUGUI gliderCostText;
    [SerializeField] private TextMeshProUGUI bounceCostText;

    // Start is called before the first frame update
    void Start()
    {
        UpgradeManager.instance.CannonCostChangedEvent += CannonCostChangeHandler;
        UpgradeManager.instance.GliderCostChangedEvent += GliderCostChangeHandler;
        UpgradeManager.instance.SlapCostChangedEvent += BounceCostChangeHandler;

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CannonCostChangeHandler(int cost)
    {
        cannonCostText.text = cost.ToString();
    }

    private void GliderCostChangeHandler(int cost)
    {
        gliderCostText.text = cost.ToString();
    }

    private void BounceCostChangeHandler(int cost)
    {
        bounceCostText.text = cost.ToString();
    }
}
