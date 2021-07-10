using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponsManager : MonoBehaviour
{
    [SerializeField] private List<ProjectileWeapon> playerWeapons;
    public IEnumerable<ProjectileWeapon> CurrentWeapons => playerWeapons;

    [SerializeField]
    private Transform leftGrip;
    [SerializeField]
    private Transform rightGrip;

    private int currentWeaponIndex;

    public WeaponSwitchingUIScript weaponSwitchingUIScript;
    
    private void Awake()
    {
        this.currentWeaponIndex = 0;
        UpdateGrip();
    }

    private void Update()
    {
        UpdateGrip();
    }

    private void UpdateGrip()
    {
        leftGrip.transform.position = playerWeapons[currentWeaponIndex].leftGrip.position;
        leftGrip.transform.rotation = playerWeapons[currentWeaponIndex].leftGrip.rotation;

        rightGrip.transform.position = playerWeapons[currentWeaponIndex].rightGrip.position;
        rightGrip.transform.rotation = playerWeapons[currentWeaponIndex].rightGrip.rotation;
    }

    private void UpdateUI()
    {
        weaponSwitchingUIScript.ShowUI(playerWeapons[currentWeaponIndex - 1 < 0 ? playerWeapons.Count - 1 : currentWeaponIndex - 1 ].displayName,
            playerWeapons[currentWeaponIndex].displayName,
            playerWeapons[(currentWeaponIndex + 1) % playerWeapons.Count].displayName);
    }
    
    public void NextWeapon()
    {
        int recentWeaponIdx = this.currentWeaponIndex;
		this.playerWeapons[this.currentWeaponIndex++].gameObject.SetActive(false);
        if (this.currentWeaponIndex >= playerWeapons.Count)
        {
            this.currentWeaponIndex = 0;
        }
        this.playerWeapons[this.currentWeaponIndex].gameObject.SetActive(true);

        UpdateGrip();
    
		if (this.currentWeaponIndex != recentWeaponIdx)
		{
			SoundManager.Instance.PlaySfx(SoundManager.SfxType.PlayerWeaponChange);
		}

        UpdateUI();
    }
    
    public void PreviousWeapon()
    {
		int recentWeaponIdx = this.currentWeaponIndex;
		this.playerWeapons[this.currentWeaponIndex--].gameObject.SetActive(false);
        if (this.currentWeaponIndex < 0)
        {
            this.currentWeaponIndex = this.playerWeapons.Count - 1;
        }
        this.playerWeapons[this.currentWeaponIndex].gameObject.SetActive(true);

        UpdateGrip();
		
		if (this.currentWeaponIndex != recentWeaponIdx)
		{
			SoundManager.Instance.PlaySfx(SoundManager.SfxType.PlayerWeaponChange);
		}

        UpdateUI();
    }

    public void ShootActiveWeapon()
    {
        this.playerWeapons[this.currentWeaponIndex].Shoot();
    }

    public void ReloadActiveWeapon()
    {
        this.playerWeapons[this.currentWeaponIndex].Reload();
    }

    public ProjectileWeapon GetCurrentWeapon()
    {
        return playerWeapons[currentWeaponIndex];
    }

    public void AddWeapon(ProjectileWeapon newWeapon)
    {
        playerWeapons.Add(newWeapon);
    }
}
