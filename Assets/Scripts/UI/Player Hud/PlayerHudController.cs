using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHudController : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI pointsText;

    private PlayerGunInventory gunInventory;
    private PlayerPoints points;
    private GunInfo currentGunInfo;
    // Start is called before the first frame update
    void Start()
    {
        gunInventory = gameObject.GetComponent<PlayerGunInventory>();
        points = gameObject.GetComponent<PlayerPoints>();
    }

    // Update is called once per frame
    void Update()
    {
        GunInfo info = gunInventory.GetCurrentGun().GetGunInfo();
        ammoText.text = $"{info.currentAmmo}/{info.magSize}";

        pointsText.text = $"{points.GetPoints()}";
        
    }
}
