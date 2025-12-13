using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Firebase.Database;
using Firebase.Auth;


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
    public GameObject basePetModel;
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
        if (isDead) return;
        petData.happiness += 1;
        LevelCheck();
        UpdateUI();
        Debug.Log("Pet action on object: " + gameObject.name);


        SaveToFirebase();
    }

    // FEEDING BUTTON
    public void FeedAction()
    {
        if (isDead) return;
        food++;
        if (food > maxFood) food = maxFood;

        UpdateFoodUI();
        Debug.Log("Fed pet. Hunger now: " + food);

        SaveToFirebase();
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
            SaveToFirebase();
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


    public void LoadFromFirebase(Pet loadedPet)
    {
        petData = loadedPet;
        UpdateUI();

        if (petData.isEvolved)
            DoEvolve();

        if (petData.isDead)
            Die();
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


    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathDelay);
        Die();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= hungerDecreaseRate)
        {
            timer = 0f;
            food--;
        if (food == 0 && !isDead)
        {
            StartCoroutine(DeathRoutine());
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
        if (isDead) return;
        petData.happiness += amount;
        if (petData.happiness > happinessToLevel) petData.happiness = happinessToLevel;
        UpdateUI();
    }
    public void OnTriggerEnter(Collider other)
    {
        Toy toy = other.GetComponent<Toy>();
        if (toy != null && toy.targetPetTag == petData.petType)
        {
            ApplyHappiness(toy.happinessAmount);
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
        if (basePetModel != null)
            basePetModel.SetActive(false);

        // Show evolved model
        if (evolvedPetInstance == null && evolvedPetModel != null)
        {
            evolvedPetInstance = Instantiate(
            evolvedPetModel,
            basePetModel.transform.position,
            evolvedPetModel.transform.rotation,
            basePetModel.transform.parent
            );
        }
        else if (evolvedPetInstance != null)
        {
            evolvedPetInstance.SetActive(true);
        }

        evolveButton.gameObject.SetActive(false);

        petData.isEvolved = true;
        SaveToFirebase();
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
            evolutionParticles.transform.position = basePetModel.transform.position;
            evolutionParticles.transform.SetParent(basePetModel.transform.parent, true);
            evolutionParticles.Play();
        }

        yield return new WaitForSeconds(evolutionDelay);

        // Call the ORIGINAL evolve logic AFTER delay
        DoEvolve();

        // Let particles continue briefly after evolution
        yield return new WaitForSeconds(2f);

    }

    [Header("Death Settings")]
    public float deathDelay = 3f;
    private bool isDead = false;
    private void Die()
    {
        if (isDead) return;
        isDead = true;
        transform.localRotation *= Quaternion.Euler(-90f, 0f, 0f);

        // Apply dead color to base pet
        if (basePetModel != null)
            ApplyDeadVisual(basePetModel);

        // Apply dead color to evolved pet (if exists)
        if (evolvedPetInstance != null)
            ApplyDeadVisual(evolvedPetInstance);

        interactMenu.SetActive(false);

        Debug.Log("Pet has died.");
        petData.isDead = true;
        SaveToFirebase();
    }

    [Header("Death Visuals")]
    public Color deadColor = new Color(0.6f, 0.1f, 0.1f);
    private void ApplyDeadVisual(GameObject root)
    {
        Renderer[] renderers = root.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            if (r.material != null)
                    r.material.color = deadColor;
        }

        
     }


    private void SaveToFirebase()
    {
        if (Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser == null) return;

        string uid = Firebase.Auth.FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        DatabaseReference db = Firebase.Database.FirebaseDatabase.DefaultInstance.RootReference;

        db.Child("Players")
        .Child(uid)
        .Child("pets")
        .Child(petData.petType)
        .SetRawJsonValueAsync(JsonUtility.ToJson(petData));
    }
}
