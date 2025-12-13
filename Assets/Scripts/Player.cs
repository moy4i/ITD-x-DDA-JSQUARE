using System.Collections.Generic;

[System.Serializable]
public class Player
{
    public string id;                // Firebase UID
    public string username;
    public Dictionary<string, Pet> pets;

    public Player(string uid, string name)
    {
        id = uid;
        username = name;
        pets = new Dictionary<string, Pet>();
    }
}
