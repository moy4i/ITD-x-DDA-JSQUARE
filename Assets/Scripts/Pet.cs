using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Pet
{
    public string petType;   // "dragon", "cat", "dog"
    public int level;
    public int happiness;
    public int hunger;

    public Pet(string type)
    {
        petType = type;
        level = 1;
        happiness = 0;
        hunger = 100;
    }

    // Happiness required per level (example)
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
