using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonShop : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private Image weaponImagePanel;
    [SerializeField] private Sprite gunSprite;

    private void ChangeDIsplayImage()
    {
        if (weaponImagePanel != null)
        {
            weaponImagePanel.sprite = gunSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChangeDIsplayImage();
    }

   
}
