using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Methods for main menu UI buttons. Used to eiher play the game as a host or client or start a dedicated server.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName = "Gameplay";

    /// <summary>
    /// Starts the game as a host
    /// </summary>
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Starts a dedicated server
    /// </summary>
    public void StartServer() 
    {
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.SceneManager.LoadScene(gameplaySceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Starts the game as a client
    /// </summary>
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
