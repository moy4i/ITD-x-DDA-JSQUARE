using UnityEngine;

public class SideMenuController : MonoBehaviour
{
    public GameObject sideMenu;
    public GameObject petsOwnedPage;
    public GameObject toysPage;

    // --- Open the side menu ---
    public void OpenMenu()
    {
        sideMenu.SetActive(true);
    }

    // --- Close the side menu ---
    public void CloseMenu()
    {
        sideMenu.SetActive(false);
    }

    // --- Open Pets Owned page ---
    public void OpenPetsOwned()
    {
        petsOwnedPage.SetActive(true);
        toysPage.SetActive(false); // optional, prevents overlap
    }

    // --- Open Toys page ---
    public void OpenToysPage()
    {
        toysPage.SetActive(true);
        petsOwnedPage.SetActive(false); // optional
    }

    public void ClosePage()
    {
        petsOwnedPage.SetActive(false);
        toysPage.SetActive(false);
    }
}
