using TMPro;
using UnityEngine;
using Firebase.Auth;
using System.Threading.Tasks;
using Firebase.Extensions;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DatabaseController : MonoBehaviour
{

    public TMP_InputField SignInEmailInput, SignInPasswordInput, signUpEmailInput, signUpPasswordInput, signUpUserName;
    public GameObject loginPanel, signUpPanel, notificationPanel;
    
    public TextMeshProUGUI notif_Title_Text, notif_Message_Text;


    public void OpenLoginPanel()
    {
        loginPanel.SetActive(true);
        signUpPanel.SetActive(false);
    }
    public void OpenSignUpPanel()
    {
        loginPanel.SetActive(false);
        signUpPanel.SetActive(true);
    }

    private void showNotificationMessage(string title, string message)
    {
        notif_Title_Text.text = "" + title;
        notif_Message_Text.text = "" + message;
    
        notificationPanel.SetActive(true);
    }
    public void SignUp()
    {
        if (string.IsNullOrEmpty(signUpEmailInput.text)&&string.IsNullOrEmpty(signUpPasswordInput.text))
        {   
            showNotificationMessage("Error","Email or Password is empty");
            return;
        }
       
       
       var createTask= FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(signUpEmailInput.text, signUpPasswordInput.text);

       createTask.ContinueWithOnMainThread(task =>
       {
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error creating user!");
            showNotificationMessage("Error","Error creating user!");
            return;
        }

        if (task.IsCompleted)
        {
            Debug.Log("User created successfully, please sign in!");
            showNotificationMessage("Success","User created successfully, please sign in!");

            var uid = task.Result.User.UserId;
            Debug.Log($"User ID: {uid}");   
   
            var player = new Player(uid, "Name");
        }

        
        });
    }

    public void SignIn()
    {
        if (string.IsNullOrEmpty(SignInEmailInput.text)&&string.IsNullOrEmpty(SignInPasswordInput.text))
            {   
                showNotificationMessage("Error","Email or Password is empty");
                return;
            }
        
       
       
       var createTask= FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(SignInEmailInput.text, SignInPasswordInput.text);

       createTask.ContinueWithOnMainThread(task =>
       {
        if (string.IsNullOrEmpty(SignInEmailInput.text)&&string.IsNullOrEmpty(SignInPasswordInput.text))
        {   
            showNotificationMessage("Error","Email or Password is empty");
            return;
        }
        if (task.IsFaulted || task.IsCanceled)
        {
            Debug.LogError("Error signing in user!");
            showNotificationMessage("Error","Error signing in user!");
            return;
        }
        if (task.IsCompleted)
        {
            Debug.Log("User signed in successfully!");
            showNotificationMessage("Success","User signed in successfully!");

            var uid = task.Result.User.UserId;
            Debug.Log($"Signed in user UID: {uid}");   

            
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainScene");
        }
        });

        
    }

    public void CloseNotifPanel()
    {

        notif_Title_Text.text = "";
        notif_Message_Text.text = "";
        notificationPanel.SetActive(false);
    }

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

}
