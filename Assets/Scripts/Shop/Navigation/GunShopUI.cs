using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShopUI : MonoBehaviour  
{
    [SerializeField] private GameObject gunsCategoryBorder;
    [SerializeField] private GameObject consumableCategoryBorder;
    [SerializeField] private GameObject categoriesPanel;
    [SerializeField] private GameObject gunBuyArea;
    [SerializeField] private GameObject consumableBuyArea;

    [SerializeField] private GameObject decisionPopUp;
    [SerializeField] private GameObject changeWeaponPopUp;
    public void ActivateGunsCategory() {
        consumableCategoryBorder.SetActive(false);
        gunsCategoryBorder.SetActive(true);
        consumableBuyArea.SetActive(false);
        gunBuyArea.SetActive(true);
    }
    public void ActivateConsumableCategory()
    {
        consumableCategoryBorder.SetActive(true);
        gunsCategoryBorder.SetActive(false);
        consumableBuyArea.SetActive(true);
        gunBuyArea.SetActive(false);
    }



    public void GoToCategoriesPanel()
    {
        categoriesPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void PopUp(bool isYes)
    {
        if (isYes)
        {
            changeWeaponPopUp.SetActive(true);
        }
        decisionPopUp.SetActive(false);
        
    }

    public void OpenDecisionPopUp()
    {
        decisionPopUp.SetActive(true);
    }

    public void ClosePopUp()
    {
        changeWeaponPopUp.SetActive(false);
    }
}
