using System.Collections.Generic;

[System.Serializable]
public class Player
{
    public string id;                // Firebase UID
    public string username;
    public List<Pet> pets;    // MANY pets

    public Player(string uid, string name)
    {
        id = uid;
        username = name;
        pets = new List<Pet>();
    }
}
