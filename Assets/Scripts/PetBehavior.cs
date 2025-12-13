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
    public Button petButton;
    public Button feedButton;
    public Slider happinessBar;
    public TextMeshProUGUI levelText;

    [Header("Settings")]
    public int maxLevel = 10;
    public int happinessToLevel = 100;  // Each level requires more happiness

    [Header("Evolution")]
    public MeshRenderer basePetMeshRenderer;
    public GameObject evolvedPetModel;
    private GameObject evolvedPetInstance;
    public Button evolveButton;

    [Header("Evolution Settings")]
    public ParticleSystem evolutionParticles;   // Assign your particle prefab here
    public float evolutionDelay = 2.5f;      // Seconds before swapping models



    void Start()
    {
        if (evolveButton != null)
        evolveButton.gameObject.SetActive(false);

        // Initialize UI
        UpdateUI();
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
        LevelCheck();
        UpdateUI();
        Debug.Log("Pet action on object: " + gameObject.name);

        // SaveToFirebase();
    }

    // FEEDING BUTTON
    public void FeedAction()
    {
        food++;
        if (food > maxFood) food = maxFood;

        UpdateFoodUI();
        Debug.Log("Fed pet. Hunger now: " + food);
    }

    // LEVEL-UP LOGIC
    public void LevelCheck()
    {
        if (petData.level >= maxLevel) return;

        if (petData.happiness >= happinessToLevel)
        {
            petData.happiness = 0; // reset happiness for next level
            petData.level++;
            levelText.text = "LVL " + petData.level;
            CheckEvolveEligibility();
        }
    }

    // Update sliders and text
    public void UpdateUI()
    {

        if (happinessBar != null)
            happinessBar.value = petData.happiness / (float)happinessToLevel;

        if (levelText != null)
            levelText.text = petData.level.ToString();

        Debug.Log("UI update: Happiness=" + petData.happiness + " Hunger=" + petData.hunger);
    }



    //Hunger Bar Section
    [Header("Hunger Bar")]
    public int food = 5;
    public int maxFood = 5;
    private float hungerDecreaseRate = 60f; // seconds
    public Sprite emptyFood;
    public Sprite fullFood;
    public Image[] foodImages;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= hungerDecreaseRate)
        {
            timer = 0f;
            food--;
            if (food == 0)
            {
                Debug.Log("Pet has died of hunger.");
                // Handle death screen or deletion here
                // SaveToFirebase();
            }
            UpdateFoodUI();
        }

        for (int i = 0; i < foodImages.Length; i++)
        {

            if (i < food)
            {
                foodImages[i].sprite = fullFood;
            }
            else
            {
                foodImages[i].sprite = emptyFood;
            }

            if (i < maxFood)
            {
                foodImages[i].enabled = true;

            }
            else
            {
                foodImages[i].enabled = false;
            }
        }
    }

    void UpdateFoodUI()
    {
        for (int i = 0; i < foodImages.Length; i++)
        {
            if (i < food)
                foodImages[i].sprite = fullFood;
            else
                foodImages[i].sprite = emptyFood;

            foodImages[i].enabled = i < maxFood;
        }
    }

    public void ApplyHappiness(int amount)
    {
        petData.happiness += amount;
        if (petData.happiness > happinessToLevel) petData.happiness = happinessToLevel;
        UpdateUI();
    }
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered trigger: " + other.name);

        if (other.CompareTag("Toy"))
        {
            Debug.Log("Toy touched pet: " + petData.petType);
            ApplyHappiness(10);
            Destroy(other.gameObject);
        }
    }

    // EVOLUTION LOGIC
    public void CheckEvolveEligibility()
    {
        if (petData.level >= 10)
            evolveButton.gameObject.SetActive(true);
    }

    private void DoEvolve()
    {
        // Hide the base model only
        if (basePetMeshRenderer != null)
            basePetMeshRenderer.enabled = false;

        // Show evolved model
        if (evolvedPetInstance == null && evolvedPetModel != null)
        {
            evolvedPetInstance = Instantiate(
                evolvedPetModel,
                basePetMeshRenderer.transform.position,
                evolvedPetModel.transform.rotation,
                basePetMeshRenderer.transform.parent
            );
        }
        else if (evolvedPetInstance != null)
        {
            evolvedPetInstance.SetActive(true);
        }

        evolveButton.gameObject.SetActive(false);

        Debug.Log("Pet evolved!");
    }


    public void EvolvePet()
    {
        StartCoroutine(EvolutionParticlesRoutine());
    }


    private IEnumerator EvolutionParticlesRoutine()
    {
        if (evolutionParticles != null) 
        {
            evolutionParticles.transform.position = basePetMeshRenderer.transform.position;
            evolutionParticles.transform.SetParent(basePetMeshRenderer.transform.parent, true);
            evolutionParticles.Play();
        }

        yield return new WaitForSeconds(evolutionDelay);

        // Call the ORIGINAL evolve logic AFTER delay
        DoEvolve();

        // Let particles continue briefly after evolution
        yield return new WaitForSeconds(2f);

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
