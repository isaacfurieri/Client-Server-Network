using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject LoginView;
    public GameObject ChatView;

    public static UIController Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TCPServer.OnServerCreated += ShowChat;
        ShowLogin();
    }

    void Update()
    {
        TCPClient.OnConnectedToServer += ShowChat;
    }

    public void ShowLogin()
    {
        UnityMainThread.umt.AddJob(() => {
            LoginView.SetActive(true);
            ChatView.SetActive(false);
        });
    }

    public void ShowChat()
    {
        UnityMainThread.umt.AddJob(() =>
        {
            LoginView.SetActive(false);
            ChatView.SetActive(true);
        });
    }

}