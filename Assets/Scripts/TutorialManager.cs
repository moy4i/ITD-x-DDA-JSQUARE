using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialPages;  // assign all tutorial panels in order
    private int currentPage = 0;

    void Start()
    {
        ShowPage(0);
    }

    public void NextPage()
    {
        currentPage++;

        // If we reached the last page, do nothing here
        if (currentPage >= tutorialPages.Length)
            return;

        ShowPage(currentPage);
    }

    public void StartGame()
    {
        // Hide all tutorial pages
        foreach (GameObject page in tutorialPages)
        {
            page.SetActive(false);
        }

        // Enable your AR camera or AR session here
        // Example (if AR camera is disabled at start):
        // arCamera.SetActive(true);

        Debug.Log("Game Started!");
    }

    private void ShowPage(int index)
    {
        // Hide all first
        for (int i = 0; i < tutorialPages.Length; i++)
        {
            tutorialPages[i].SetActive(i == index);
        }
    }

    public void ShowTutorial()
{
    // Reset to the first page
    currentPage = 0;

    // Show page 1
    ShowPage(0);

    // If your AR camera or AR session is disabled during tutorial:
    // arSessionOrigin.SetActive(false);

    Debug.Log("Tutorial Opened");
}

}
