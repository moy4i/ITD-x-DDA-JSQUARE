using UnityEngine;
using UnityEngine.UI;

public class InteractMenuController : MonoBehaviour
{
    [Header("Menu UI")]
    public GameObject interactMenu;  // Panel with buttons
    public Button petButton;
    public Button feedButton;

    private bool isOpen = false;

    void Start()
    {
        interactMenu.SetActive(false);

        // Assign button clicks
        petButton.onClick.AddListener(OnPetButton);
        feedButton.onClick.AddListener(OnFeedButton);
    }

    // Show the interaction menu
    public void ShowMenu()
    {
        if (isOpen) return;

        interactMenu.SetActive(true);
        isOpen = true;
    }

    // Hide the interaction menu
    public void HideMenu()
    {
        if (!isOpen) return;

        interactMenu.SetActive(false);
        isOpen = false;
    }

    private void OnPetButton()
    {
        Debug.Log("Pet button clicked!");
        // You will later call: Pet your pet, increase happiness, animate etc.
    }

    private void OnFeedButton()
    {
        Debug.Log("Feed button clicked!");
        // You will later call: Increase hunger bar, reduce food inventory, etc.
    }
}
