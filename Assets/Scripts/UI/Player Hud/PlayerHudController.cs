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
    private Target target;

    private List<Gun> playerGuns;

    // Start is called before the first frame update
    void Start()
    {
        gunInventory = gameObject.GetComponent<PlayerGunInventory>();
        points = gameObject.GetComponent<PlayerPoints>();
        target = gameObject.GetComponent<Target>();
    }

    // Update is called once per frame
    void Update()
    {
        currentGunInfo = gunInventory.GetCurrentGun().GetGunInfo();
        ammoText.text = $"{currentGunInfo.currentAmmo}/{currentGunInfo.magSize}";
        pointsText.text = $"{points.GetPoints()}";
        healthText.text = $"{target.GetHealth()}";
        
    }
}
