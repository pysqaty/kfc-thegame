using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoCounterScript : MonoBehaviour
{
    public PlayerWeaponsManager weaponsManager;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmoText();
    }

    private void UpdateAmmoText()
    {
        ProjectileWeapon currentWeapon = weaponsManager.GetCurrentWeapon();
        text.color = currentWeapon.Reloading ? Color.red : Color.white;
        if(currentWeapon.stats.ammoCapacity == -1)
        {
            text.text = "Inf";
        }
        else
            text.text = currentWeapon.AmmoLeft.ToString() + "/" + currentWeapon.stats.ammoCapacity.ToString();   
    }
}
