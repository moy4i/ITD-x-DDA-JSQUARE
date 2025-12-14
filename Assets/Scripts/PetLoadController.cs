using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;

public class PetLoadController : MonoBehaviour
{
    public PetBehaviour[] petsInScene;

    void Start()
    {
        LoadPetsFromFirebase();
    }

    void LoadPetsFromFirebase()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
            return;

        string uid = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        FirebaseDatabase.DefaultInstance
            .GetReference("Players")
            .Child(uid)
            .Child("pets")
            .GetValueAsync()
            .ContinueWithOnMainThread(task =>
            {
                if (!task.Result.Exists) return;

                foreach (var petSnapshot in task.Result.Children)
                {
                    string json = petSnapshot.GetRawJsonValue();
                    Pet loadedPet = JsonUtility.FromJson<Pet>(json);

                    foreach (var petBehaviour in petsInScene)
                    {
                        if (petBehaviour.petData.petType == loadedPet.petType)
                        {
                            petBehaviour.LoadFromFirebase(loadedPet);
                            break;
                        }
                    }
                }

            });
    }
}

