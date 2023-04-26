using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeShopUI : MonoBehaviour
{
    [SerializeField] private GameObject gunOption;
    [SerializeField] private GameObject playerOption;
    [SerializeField] private GameObject categoriesPanel;
    [SerializeField] private GameObject upgradePanel;
    [SerializeField] private GameObject menuUpgradePanel;

    [SerializeField] private GameObject gunsArea;
    [SerializeField] private GameObject upgradeArea;


    public void OpenGunOption()
    {
        gunOption.SetActive(true);
        gameObject.SetActive(false);
        
    }

    public void OpenPlayerOption()
    {
        playerOption.SetActive(true);
        gameObject.SetActive(false);
    }
    public void GoToCategoriesPanel()
    {
        categoriesPanel.SetActive(true);
        upgradePanel.SetActive(false);
    }

    public void GoToMenuUpgradePanel()
    {
        if(gunsArea != null && upgradeArea !=null) {
            gunsArea.SetActive(true);
            upgradeArea.SetActive(false);
        }
        menuUpgradePanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenGunUpgradeArea()
    {
        gunsArea.SetActive(false);
        upgradeArea.SetActive(true);
    }

    
}
