[System.Serializable]
public class Pet
{
    public string petType;   // "dragon", "cat", "dog"
    public int level;
    public int hunger;
    public int happiness;

    public Pet(string type)
    {
        petType = type;
        level = 1;
        hunger = 100;
        happiness = 100;
    }
}
