using UnityEngine;

public class SideMenuController : MonoBehaviour
{
    public GameObject sideMenu;
    public GameObject petsOwnedPage;
    public GameObject toysPage;
    public GameObject infoPageDragon;
    public GameObject infoPageCat;  
    public GameObject infoPageDog;

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

    public void OpenDragonInfoPage()
    {
        infoPageDragon.SetActive(true);
        infoPageCat.SetActive(false);
        infoPageDog.SetActive(false);
    }
    public void OpenCatInfoPage()
    {
        infoPageCat.SetActive(true);
        infoPageDragon.SetActive(false);
        infoPageDog.SetActive(false);
    }
    public void OpenDogInfoPage()
    {
        infoPageDog.SetActive(true);
        infoPageDragon.SetActive(false);
        infoPageCat.SetActive(false);
    }
    
    public void CloseInfoPages()
    {
        infoPageDragon.SetActive(false);
        infoPageCat.SetActive(false);
        infoPageDog.SetActive(false);
    }
}
