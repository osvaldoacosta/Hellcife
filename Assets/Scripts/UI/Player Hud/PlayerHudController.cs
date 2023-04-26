using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

using UnityEngine.UI;
public class PlayerHudController : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI pointsText;
    [SerializeField] private GameObject emptySlotPrefab;
    [SerializeField] private RectTransform panel;
    public List<Button> gunsButtons;
    public Color highlightButtonColor;
    
    private Color originalColor;
    private PlayerPoints points;
    private GunInfo currentGunInfo;
    private Target target;
    public void ExtendInventoryHud()
    {
        //Da resize no painel das armas

        panel.sizeDelta = new Vector2(panel.sizeDelta.x + 128f, panel.sizeDelta.y);

        // Get the number of children of the parent panel
        int childCount = panel.childCount;

        for (int i = 0; i < childCount; i++)
        {
            RectTransform childRectTransform = panel.GetChild(i).GetComponent<RectTransform>();

            // Move the child RectTransform
            Vector3 newPosition = childRectTransform.localPosition;
            newPosition.x -= 64f;
            childRectTransform.localPosition = newPosition;
        }
        //Adding a new child
        RectTransform newSlotTransform = Instantiate(emptySlotPrefab, panel).GetComponent<RectTransform>();
        Debug.Log(newSlotTransform.name);
        Vector3 newPosi = newSlotTransform.localPosition;
        newPosi.x += 128f; // Posicao relativa ao lugar do painel
        newSlotTransform.localPosition = newPosi;
        Button newSlotButton = newSlotTransform.GetComponentInChildren<Button>();
        gunsButtons.Add(newSlotButton);
        gameObject.GetComponent<PlayerGunInventory>().IncreaseCarryingSize();
    }


    void Start()
    {
        points = gameObject.GetComponent<PlayerPoints>();
        target = gameObject.GetComponent<Target>();
        PlayerGunInventory.onWeaponChange += WeaponChange;
        originalColor = gunsButtons[0].GetComponent<Image>().color;
        currentGunInfo = gameObject.GetComponent<PlayerGunInventory>().GetCurrentGun()?.GetGunInfo();
        InitialWeaponChange();
    }

    
    void Update()
    {
        
        ChangeGunStats(); //Quick fix, o certo seria no reload e no tiro ter um action.
        pointsText.text = $"{points.GetPoints()}";
        healthText.text = $"{target.GetHealth()}";
    }

    private void WeaponChange(ushort index, Gun currentGun){

        currentGunInfo = currentGun?.GetGunInfo();
        ChangeGunStats(); 
        UnhighlightBtns();
        HighlightBtn(index);
    }

    public void InitialWeaponChange(){
        ammoText.text = $"{currentGunInfo.currentAmmo}/{currentGunInfo.magSize}";
        UnhighlightBtns();
        HighlightBtn(0);
    }

    private void ChangeGunStats(){
        if(currentGunInfo == null){
          ammoText.text = $"--/--"; 
        }
        else ammoText.text = $"{currentGunInfo.currentAmmo}/{currentGunInfo.magSize}";
    }

    private void UnhighlightBtns() {
      foreach (Button btn in gunsButtons)
      {
        btn.GetComponent<Image>().color = originalColor;  
      } 
    }

    private void HighlightBtn(ushort index) => gunsButtons[index].GetComponent<Image>().color = highlightButtonColor;
}
