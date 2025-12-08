using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIFucntions : MonoBehaviour
{
    [Header("Main Stuffs")]
    [SerializeField] private List<string> Stalls;
    [Header("Settings Stuffs")]
    [SerializeField] private GameObject SettingsMenu;
    [Header("Upgrades Menu Stuffs")]
    [SerializeField] private GameObject UpgradesMenu;
    [SerializeField] private GameObject UpgradePanel;
    [SerializeField] private GameObject spawnParent;
    [Header("Upgrades Menu Stuffs")]
    [SerializeField] private GameObject ShopMenu;
    [SerializeField] private GameObject ShopPanel;
    [SerializeField] private GameObject ShopParent;

    public void loadMainMenu() {
        SceneManager.LoadScene("GameMenu");
    }

    public void ToggleSettings() {
        if (!SettingsMenu.activeSelf) {
            SettingsMenu.SetActive(true);
            SettingsMenu.GetComponent<Animator>().Play("Settings");
        } else {
            StartCoroutine(CloseAfterAnim(SettingsMenu));
        }
    }

    public void ToggleUpgrades() {
        if (!UpgradesMenu.activeSelf) {
            UpgradesMenu.SetActive(true);
            UpgradesMenu.GetComponent<Animator>().Play("Settings");

            // Spawn Upgrade Panels
            foreach (string name in Stalls) {
                GameObject upgPanel = Instantiate(UpgradePanel, spawnParent.transform.position, Quaternion.identity, spawnParent.transform);
                var script = upgPanel.GetComponent<UpgradesMenuScript>();
                script.setProductName(name);
                script.setStockCount(1000);
                script.setLevelPrice(1000);
                script.setStockUpgradePrice(1000);
                script.setPanelDetails();
            }
            spawnParent.transform.position = new Vector3(
                spawnParent.transform.position.x,
                -(spawnParent.transform.localScale.y / 2),
                spawnParent.transform.position.z
            );
        } else {
            StartCoroutine(CloseAfterAnim(UpgradesMenu));
        }
    }

    public void ToggleShop() {
        if (!ShopMenu.activeSelf) {
            ShopMenu.SetActive(true);
            ShopMenu.GetComponent<Animator>().Play("Settings");

            // Spawn Upgrade Panels
            //foreach (string name in Stalls) {
            //    GameObject upgPanel = Instantiate(UpgradePanel, spawnParent.transform.position, Quaternion.identity, spawnParent.transform);
            //    var script = upgPanel.GetComponent<UpgradesMenuScript>();
            //    script.setProductName(name);
            //    script.setStockCount(1000);
            //    script.setLevelPrice(1000);
            //    script.setStockUpgradePrice(1000);
            //    script.setPanelDetails();
            //}
            //spawnParent.transform.position = new Vector3(
            //    spawnParent.transform.position.x,
            //    -(spawnParent.transform.localScale.y / 2),
            //    spawnParent.transform.position.z
            //);
        } else {
            StartCoroutine(CloseAfterAnim(ShopMenu));
        }
    }

    IEnumerator CloseAfterAnim(GameObject gameObj) {
        Animator anim = gameObj.GetComponent<Animator>();
        anim.Play("SettingsClose");

        if (gameObj.Equals(UpgradesMenu)) {
            foreach (Transform t in spawnParent.transform) {
                Destroy(t.gameObject);
            }
        }

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        gameObj.SetActive(false);
    }
}
