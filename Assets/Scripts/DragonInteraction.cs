using UnityEngine;

public class DragonInteraction : MonoBehaviour
{
    public ParticleSystem heartParticles;
    public ParticleSystem sparkleParticles;

    public void FeedDragon()
    {
        Debug.Log("FeedDragon() called on " + name);
        if (heartParticles != null)
        {
            Debug.Log("Playing heart particles");
            heartParticles.Play();
        }
        else
            Debug.LogWarning("Heart particles not assigned!");
    }

    public void PetDragon()
    {
        Debug.Log("PetDragon() called on " + name);
        if (sparkleParticles != null)
        {
            Debug.Log("Playing sparkle particles");
            sparkleParticles.Play();
        }
        else
            Debug.LogWarning("Sparkle particles not assigned!");
    }
}


