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
        menuUpgradePanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
