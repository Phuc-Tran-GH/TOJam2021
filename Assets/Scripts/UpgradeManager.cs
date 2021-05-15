using System;
using UnityEngine;
using UnityEngine.Networking;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public GameObject upgradePanel;

    public int[] cannonUpgadeCosts;
    public int[] gliderUpgradeCosts;
    public int[] slapUpgradeCosts;

    private int cannonUpgradeNum = 0;
    private int gliderUpgradeNum = 0;
    private int slapUpgradeNum = 0;

    public event Action<int> CannonCostChangedEvent;
    public event Action<int> GliderCostChangedEvent;
    public event Action<int> SlapCostChangedEvent;

    // Start is called before the first frame update
    void Start()
    {
        CannonCostChangedEvent?.Invoke(GetCannonUpgradeCost());
        GliderCostChangedEvent?.Invoke(GetGliderUpgradeCost());
        SlapCostChangedEvent?.Invoke(GetSlapUpgradeCost());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCannonUpgradeCost()
    {
        return cannonUpgadeCosts[cannonUpgradeNum];
    }

    public int GetGliderUpgradeCost()
    {
        return gliderUpgradeCosts[gliderUpgradeNum];
    }

    public int GetSlapUpgradeCost()
    {
        return slapUpgradeCosts[slapUpgradeNum];
    }

    public void UpgradeCannon()
    {
        if (cannonUpgradeNum < cannonUpgadeCosts.Length && GameManager.instance.Wood >= GetCannonUpgradeCost())
        {
            GameManager.instance.SpendWood(GetCannonUpgradeCost());
            cannonUpgradeNum++;
            CannonCostChangedEvent?.Invoke(GetCannonUpgradeCost());
        }
        else
        {
            //DO NOTHING
        }
    }

    public void UpgradeGlider()
    {
        if (gliderUpgradeNum < gliderUpgradeCosts.Length && GameManager.instance.Wood >= GetGliderUpgradeCost())
        {
            GameManager.instance.SpendWood(GetGliderUpgradeCost());
            gliderUpgradeNum++;
            GliderCostChangedEvent?.Invoke(GetGliderUpgradeCost());
        }
        else
        {
            //DO NOTHING
        }
    }

    public void UpgradeSlap()
    {
        if (slapUpgradeNum < slapUpgradeCosts.Length && GameManager.instance.Wood >= GetSlapUpgradeCost())
        {
            GameManager.instance.SpendWood(GetSlapUpgradeCost());
            slapUpgradeNum++;
            SlapCostChangedEvent?.Invoke(GetSlapUpgradeCost());
        }
        else
        {
            //DO NOTHING
        }
    }

    public bool HasCannonUpgrade()
    {
        return cannonUpgradeNum < cannonUpgadeCosts.Length;
    }

    public bool HasGliderUpgrade()
    {
        return gliderUpgradeNum < gliderUpgradeCosts.Length;
    }

    public bool HasSlapUpgrade()
    {
        return slapUpgradeNum < slapUpgradeCosts.Length;
    }

    public void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true);
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
        GameManager.instance.ResetBeaver();
    }

}
