[System.Serializable]
public class Player
{
    public string id;          // Firebase UID
    public string petType;     // "dragon", "cat", or "turtle"
    public int petLevel;
    public int petHunger;
    public int petHappiness;

    public Player(string uid, string chosenPet)
    {
        id = uid;
        petType = chosenPet;
        petLevel = 1;
        petHunger = 100;
        petHappiness = 100;
    }
}
