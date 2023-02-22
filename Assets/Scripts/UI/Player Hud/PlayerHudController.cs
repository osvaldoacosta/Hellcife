using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;
public class PlayerHudController : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI pointsText;

    public List<Button> gunsButtons;
    public Color highlightButtonColor;
    private Color originalColor;
    private PlayerPoints points;
    private GunInfo currentGunInfo;
    private Target target;
    


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
