using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CategoriesShopUI : MonoBehaviour
{
    [SerializeField] private GameObject buyOption;
    [SerializeField] private GameObject upgradeOption;
    [SerializeField] private GameObject categoriesPanel;
    public void OpenBuyShop()
    {
        gameObject.SetActive(false);
        buyOption.SetActive(true);
    }

    public void OpenUpgradeShop()
    {
        gameObject.SetActive(false);
        upgradeOption.SetActive(true);
    }
    public void GoToCategoriesPanel()
    {
        categoriesPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}

