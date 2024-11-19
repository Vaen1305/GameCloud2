using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DB : MonoBehaviour
{
    private string url = "http://localhost/loginUser.php";
    private string registerUrl = "http://localhost/registerUser.php";

    [SerializeField] private User user;
    [SerializeField] private ServerResponse response;

    public void Username(string username)
    {
        user.username = username;
    }

    public void Password(string password)
    {
        user.password = password;
    }

    public void Login()
    {
        StartCoroutine("LoginE");
    }

    IEnumerator LoginE()
    {
        string jsonString = JsonUtility.ToJson(user);
        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("Error: " + request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            Debug.Log("Server Response: " + responseText);
            response = JsonUtility.FromJson<ServerResponse>(responseText);

            if (response.message == "Login successful")
            {
                Debug.Log("Login successful!");
                SceneHelper.LoadScene(1);
            }
            else
            {
                Debug.Log("Login Failed");
            }
        }
    }


    public void RegisterUser(string username, string password)
    {
        StartCoroutine(RegisterUserCoroutine(username, password));
    }

    private IEnumerator RegisterUserCoroutine(string username, string password)
    {
        User newUser = new User { username = username, password = password };
        string jsonString = JsonUtility.ToJson(newUser);

        UnityWebRequest request = new UnityWebRequest("http://localhost/registerUser.php", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("User registered successfully: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error registering user: " + request.error);
        }
    }
    public void LoginUser(string username, string password)
    {
        StartCoroutine(LoginUserCoroutine(username, password));
    }

    private IEnumerator LoginUserCoroutine(string username, string password)
    {
        User loginUser = new User { username = username, password = password };
        string jsonString = JsonUtility.ToJson(loginUser);

        UnityWebRequest request = new UnityWebRequest("http://localhost/loginUser.php", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Login successful: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Login error: " + request.error);
        }
    }
    public void SaveGame1Score(int userId, int score)
    {
        StartCoroutine(SaveGame1ScoreCoroutine(userId, score));
    }

    private IEnumerator SaveGame1ScoreCoroutine(int userId, int score)
    {
        GameScore newScore = new GameScore { userid = userId, score = score };
        string jsonString = JsonUtility.ToJson(newScore);

        UnityWebRequest request = new UnityWebRequest("http://localhost/postgame1.php", "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score saved successfully: " + request.downloadHandler.text);
        }
        else
        {
            Debug.Log("Error saving score: " + request.error);
        }
    }
    public void RegisterUser()
    {
        StartCoroutine(RegisterUserCoroutine());
    }

    private IEnumerator RegisterUserCoroutine()
    {
        string jsonString = JsonUtility.ToJson(user);
        UnityWebRequest request = new UnityWebRequest(registerUrl, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonString);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            response = JsonUtility.FromJson<ServerResponse>(request.downloadHandler.text);
            Debug.Log("Register Response: " + response.message);

            if (response.message == "User registered successfully")
            {
                Debug.Log("Registration successful!");
            }
            else
            {
                Debug.Log("Registration failed: " + response.message);
            }
        }
        else
        {
            Debug.Log("Error registering user: " + request.error);
        }
    }
}

[System.Serializable]
public class User
{
    public string username;
    public string password;
}

[System.Serializable]
public class ServerResponse
{
    public string message;
}

[System.Serializable]
public class GameScore
{
    public int userid;
    public int score;
}