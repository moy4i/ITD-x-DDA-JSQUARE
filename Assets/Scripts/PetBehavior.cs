using UnityEngine;

public class PetBehaviour : MonoBehaviour
{
    public Pet petData;

    public void Init(Pet data)
    {
        petData = data;
    }

    public void OnTapped()
    {
        // Increase XP (using happiness)
        petData.happiness += 10;

        // Optional: Level up every 100 XP
        if (petData.happiness >= 100)
        {
            petData.level++;
            petData.happiness = 0; // reset XP
            Debug.Log("LEVEL UP! New Level: " + petData.level);
        }

        // Reduce hunger a bit (makes game feel alive)
        petData.hunger = Mathf.Max(0, petData.hunger - 2);

        Debug.Log("Pet tapped! XP/Happiness: " + petData.happiness);
    }
}
