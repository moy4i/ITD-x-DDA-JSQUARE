using UnityEngine;

public class SideMenuController : MonoBehaviour
{
    public GameObject HamburgerMenu;  // assign your panel here

    public void OpenPanel()
    {
        HamburgerMenu.SetActive(true);
    }

    public void ClosePanel()
    {
        HamburgerMenu.SetActive(false);
    }
}
