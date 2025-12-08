using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIFucntions : MonoBehaviour
{
    [SerializeField] private GameObject SettingsMenu;

    public void loadMainMenu()
    {
        SceneManager.LoadScene("GameMenu");
    }

    public void OpenSettings()
    {
        if (!SettingsMenu.activeSelf)
        {
            SettingsMenu.SetActive(true);
            SettingsMenu.GetComponent<Animator>().Play("Settings");
        }
        else
        {
            StartCoroutine(CloseAfterAnim());
        }
    }

    IEnumerator CloseAfterAnim()
    {
        Animator anim = SettingsMenu.GetComponent<Animator>();
        anim.Play("SettingsClose");

        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);

        SettingsMenu.SetActive(false);
    }
}
