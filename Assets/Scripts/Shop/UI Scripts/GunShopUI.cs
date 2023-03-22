using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShopUI : MonoBehaviour  
{
    [SerializeField] private GameObject gunsCategoryBorder;
    [SerializeField] private GameObject consumableCategoryBorder;
    [SerializeField] private GameObject categoriesPanel;
    public void ActivateGunsBorder() {
        consumableCategoryBorder.SetActive(false);
        gunsCategoryBorder.SetActive(true);
    }
    public void ActivateConsumableBorder()
    {
        consumableCategoryBorder.SetActive(true);
        gunsCategoryBorder.SetActive(false);
    }

    public void GoToCategoriesPanel()
    {
        categoriesPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
