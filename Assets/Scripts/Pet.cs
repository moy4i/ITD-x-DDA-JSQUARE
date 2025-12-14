[System.Serializable]
public class Pet
{
    public string petType;   // "dragon", "cat", "dog"
    public int level;
    public int happiness;
    public bool isEvolved;
    public bool isDead;

    public Pet(string type)
    {
        petType = type;
        level = 1;
        happiness = 0;
        isEvolved = false;
        isDead = false;
    }
    
    public int HappinessToLevelUp()
    {
        return level * 10; // Level 1 = 10, Level 2 = 20, etc.
    }

    public bool CanLevelUp()
    {
        return level < 10 && happiness >= HappinessToLevelUp();
    }

    public void LevelUp()
    {
        if (CanLevelUp())
        {
            happiness -= HappinessToLevelUp();
            level++;
        }
    }
}
