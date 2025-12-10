using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PetBehaviour : MonoBehaviour
{
    [Header("Pet Data")]
    public Pet petData;     // Assigned by your spawner or from Firebase

    [Header("UI References (Assign inside prefab)")]
    public GameObject interactMenu;
    public Slider hungerBar;
    public Slider happinessBar;
    public Slider levelBar;
    public TextMeshProUGUI levelText;

    [Header("Settings")]
    public int maxLevel = 10;
    public int happinessToLevel = 100;  // Each level requires more happiness
    public float hungerDecreaseRate = 1f;  // every X seconds
    
    void Start()
    {
        // Initialize UI
        UpdateUI();

        // Start hunger drain
        StartCoroutine(HungerRoutine());
    }

    // Called by button inside prefab
    public void ToggleInteractMenu()
    {
        interactMenu.SetActive(!interactMenu.activeSelf);
        UpdateUI();
    }

    // PETTING BUTTON
    public void PetAction()
    {
        petData.happiness += 5;
        petData.hunger -= 1;

        LevelCheck();
        UpdateUI();
        Debug.Log("Pet action on object: " + gameObject.name);

        // SaveToFirebase();
    }

    // FEEDING BUTTON
    public void FeedAction()
    {
        petData.hunger += 20;
        if (petData.hunger > 100) petData.hunger = 100;

        UpdateUI();
        Debug.Log("Pet action on object: " + gameObject.name);

        // SaveToFirebase();
    }

    // LEVEL-UP LOGIC
    private void LevelCheck()
    {
        if (petData.level >= maxLevel) return;

        if (petData.happiness >= happinessToLevel)
        {
            petData.happiness = 0; // reset happiness for next level
            petData.level++;
            levelText.text = "LVL " + petData.level;
        }
    }

    // Update sliders and text
    private void UpdateUI()
    {
        if (hungerBar != null)
            hungerBar.value = petData.hunger / 100f;

        if (happinessBar != null)
            happinessBar.value = petData.happiness / (float)happinessToLevel;

        if (levelBar != null)
            levelBar.value = petData.level / (float)maxLevel;

        if (levelText != null)
            levelText.text = "LVL " + petData.level;

        Debug.Log("UI update: Happiness=" + petData.happiness + " Hunger=" + petData.hunger);
    }

    // HUNGER GOES DOWN OVER TIME
    IEnumerator HungerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(hungerDecreaseRate);

            petData.hunger -= 2;
            if (petData.hunger < 0) petData.hunger = 0;

            // PET DIES?
            if (petData.hunger == 0)
            {
                Debug.Log("Pet has died.");
                // Handle death screen or deletion here
                // SaveToFirebase();
                yield break;
            }

            UpdateUI();
        }
    }


    

    // ---------- OPTIONAL FIREBASE SAVE ----------
    /*
    private void SaveToFirebase()
    {
        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DatabaseReference db = FirebaseDatabase.DefaultInstance.RootReference;
        string petIndex = "0"; // depends how many pets user has

        db.Child("Players").Child(uid).Child("pets").Child(petIndex).SetRawJsonValueAsync(JsonUtility.ToJson(petData));
    }
    */
}
