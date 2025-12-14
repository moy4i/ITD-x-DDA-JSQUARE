[System.Serializable]
public class PetFirebaseData
{
    public string petType;
    public int level;
    public int happiness;
    public bool isEvolved;
    public bool isDead;

    public PetFirebaseData(Pet pet)
    {
        petType = pet.petType;
        level = pet.level;
        happiness = pet.happiness;
        isEvolved = pet.isEvolved;
        isDead = pet.isDead;
    }
}
