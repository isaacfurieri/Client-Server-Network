using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject LoginView;
    public GameObject ChatView;

    public static UIController Instance;

    public bool IsServer = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TCPServer.OnServerCreated += () => { IsServer = true; ShowChat(); };
        ShowLogin();
    }

    void Update()
    {
        TCPClient.OnConnectedToServer += ShowChat;
    }

    private void ShowLogin()
    {
        UnityMainThread.umt.AddJob(() => {
            LoginView.SetActive(true);
            ChatView.SetActive(false);
        });
    }

    private void ShowChat()
    {
        UnityMainThread.umt.AddJob(() =>
        {
            LoginView.SetActive(false);
            ChatView.SetActive(true);
        });
    }

}