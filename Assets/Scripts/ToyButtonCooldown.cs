using UnityEngine;
using UnityEngine.UI;

public class ToyButtonCooldown : MonoBehaviour
{
    public Image IMG;
    public Button UseNowButton;
    int manager;

    float cooldownEndTime;
    float cooldownDuration;
    bool isOnCooldown;

    public void StartCooldown(float duration)
    {
        cooldownDuration = duration;
        cooldownEndTime = Time.time + cooldownDuration;
        isOnCooldown = true;

        IMG.fillAmount = 1;
        UseNowButton.enabled = false;
        manager = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnCooldown) return;

        float remaining = cooldownEndTime - Time.time;

        if (remaining > 0)
        {
            IMG.fillAmount = remaining / cooldownDuration;
        }
        else
        {
            IMG.fillAmount = 0f;
            UseNowButton.enabled = true;
            isOnCooldown = false;
        }
    }

}
