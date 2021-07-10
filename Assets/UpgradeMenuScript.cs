using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuScript : MonoBehaviour
{
    private class ButtonUpgradeData
    {
        public Button button;
        public WeaponUpgradesData upgrade;
        public bool bought;

        public ButtonUpgradeData(Button btn, WeaponUpgradesData data)
        {
            button = btn;
            upgrade = data;
            bought = false;
        }
    }

    [SerializeField]
    private GameObject container;
    [SerializeField]
    private GameObject template;
    [SerializeField]
    private WeaponUpgradesData[] weaponUpgrades;
    [SerializeField]
    private ProjectileWeapon projectileWeapon;


    private List<ButtonUpgradeData> upgradeButtons;

    // Start is called before the first frame update
    void Start()
    {
        upgradeButtons = new List<ButtonUpgradeData>();

        foreach(WeaponUpgradesData weaponUpgrade in weaponUpgrades)
        {
            GameObject upgrade = Instantiate(template, container.transform);
            upgrade.transform.Find("NameAndDescription/Name").GetComponent<Text>().text = weaponUpgrade.displayname;
            upgrade.transform.Find("NameAndDescription/Description").GetComponent<Text>().text = weaponUpgrade.description;
            Button button = upgrade.transform.Find("Button").GetComponent<Button>();
            button.onClick.AddListener(() => BuyUpgrade(button, weaponUpgrade));
            upgrade.SetActive(true);

            upgradeButtons.Add(new ButtonUpgradeData(button, weaponUpgrade));

            Debug.Log(upgrade.transform.Find("Button").GetComponent<Button>().transform.position);
            Debug.Log(upgrade.transform.position);
        }
    }

    public void BuyUpgrade(Button button, WeaponUpgradesData weaponUpgrade)
    {
        projectileWeapon.AddUpgrade(weaponUpgrade);

        GameManager.score -= weaponUpgrade.cost;

        upgradeButtons.Where(w => w.upgrade == weaponUpgrade).First().bought = true;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var data in upgradeButtons)
        {
            bool canAfford = GameManager.score >= data.upgrade.cost;
            data.button.interactable = canAfford && (data.bought == false);

            if(data.bought)
            {
                data.button.transform.Find("Text").GetComponent<Text>().text = "Bought";
            }
            else
            {
                data.button.transform.Find("Text").GetComponent<Text>().text = $"Buy\n{data.upgrade.cost}$";
            }
        }
    }
}
