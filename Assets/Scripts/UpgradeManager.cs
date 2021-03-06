using System;
using UnityEngine;
using UnityEngine.Networking;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public GameObject upgradePanel;

    public int[] cannonUpgadeCosts;
    public int[] gliderUpgradeCosts;
    public int[] slapUpgradeCosts;

    public float[] cannonUpgradeMultipliers;
    public float[] gliderUpgradeMultipliers;
    public float[] slapUpgradeMultipliers; 

    private int cannonUpgradeNum = 0;
    private int gliderUpgradeNum = 0;
    private int slapUpgradeNum = 0;

    public event Action<int> CannonCostChangedEvent;
    public event Action<int> GliderCostChangedEvent;
    public event Action<int> SlapCostChangedEvent;

    public CannonUpgradeIconScript cannonUpgradeIcon;
    public GliderUpgradeIconScript gliderUpgradeIcon;
    public CannonUpgradeIconScript bounceUpgradeIcon;

    public LevelText levelText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetCannonUpgradeCost()
    {
        int index = Mathf.Min(cannonUpgradeNum, cannonUpgadeCosts.Length - 1);
        return cannonUpgadeCosts[index];
    }

    public int GetGliderUpgradeCost()
    {
        int index = Mathf.Min(gliderUpgradeNum, gliderUpgradeCosts.Length - 1);
        return gliderUpgradeCosts[index];
    }

    public int GetSlapUpgradeCost()
    {
        int index = Mathf.Min(slapUpgradeNum, slapUpgradeCosts.Length - 1);
        return slapUpgradeCosts[index];
    }

    public float GetCannonUpgradeMultiplier()
    {
        return cannonUpgradeMultipliers[cannonUpgradeNum];
    }

    public float GetGliderUpgradeMultiplier()
    {
        return gliderUpgradeMultipliers[gliderUpgradeNum];
    }

    public float GetSlapUpgradeMultiplier()
    {
        return slapUpgradeMultipliers[slapUpgradeNum];
    }

    public void UpgradeCannon()
    {
        if (HasCannonUpgrade() && GameManager.instance.Wood >= GetCannonUpgradeCost())
        {
            GameManager.instance.SpendWood(GetCannonUpgradeCost());
            cannonUpgradeNum++;
            CannonCostChangedEvent?.Invoke(GetCannonUpgradeCost());
            UISounds.instance.PlayUpgradeSound();
            cannonUpgradeIcon.UpgradeIcon(cannonUpgradeNum);
        }
        else
        {
            UISounds.instance.PlayOKSound();
        }
    }

    public void UpgradeGlider()
    {
        if (HasGliderUpgrade() && GameManager.instance.Wood >= GetGliderUpgradeCost())
        {
            GameManager.instance.SpendWood(GetGliderUpgradeCost());
            gliderUpgradeNum++;
            GliderCostChangedEvent?.Invoke(GetGliderUpgradeCost());
            UISounds.instance.PlayUpgradeSound();
            gliderUpgradeIcon.UpgradeIcon(gliderUpgradeNum);
        }
        else
        {
            UISounds.instance.PlayOKSound();
        }
    }

    public void UpgradeSlap()
    {
        if (HasSlapUpgrade() && GameManager.instance.Wood >= GetSlapUpgradeCost())
        {
            GameManager.instance.SpendWood(GetSlapUpgradeCost());
            slapUpgradeNum++;
            SlapCostChangedEvent?.Invoke(GetSlapUpgradeCost());
            UISounds.instance.PlayUpgradeSound();
            
            GameManager.instance.Beaver.SetTail(slapUpgradeNum);
            bounceUpgradeIcon.UpgradeIcon(slapUpgradeNum);
        }
        else
        {
            UISounds.instance.PlayOKSound();
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

        if (HasCannonUpgrade())
        {
            CannonCostChangedEvent?.Invoke(GetCannonUpgradeCost());
        }

        if (HasGliderUpgrade())
        {
            GliderCostChangedEvent?.Invoke(GetGliderUpgradeCost());
        }

        if (HasSlapUpgrade())
        {
            SlapCostChangedEvent?.Invoke(GetSlapUpgradeCost());
        }
    }

    public void CloseUpgradePanel()
    {
        upgradePanel.SetActive(false);
        GameManager.instance.ResetBeaver();
        UISounds.instance.PlayOKSound();
    }

    public int GetCannonUpgradeNum()
    {
        return cannonUpgradeNum;
    }

    public int GetGliderUpgradeNum()
    {
        return gliderUpgradeNum;
    }

    public int GetSlapUpgradeNum()
    {
        return slapUpgradeNum;
    }

}
