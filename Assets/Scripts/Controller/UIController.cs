using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject MainScreenView;
    public GameObject CreateServerView;
    public GameObject JoinServerView;
    public GameObject ChatView;

    public static UIController Instance;

    public bool IsServer = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        TCPMyServer.OnServerCreated += () => { IsServer = true; ShowChat(); };
        ShowMainScreen();
    }

    void Update()
    {
        TCPMyClient.OnConnectedToServer += ShowChat;
        MainScreen.onServerButtonClicked += ShowCreateServer;
        MainScreen.onClientButtonClicked += ShowJoinServer;
        CreateServerScreen.onBackButtonClicked += ShowMainScreen;
        JoinServerScreen.onBackButtonClicked += ShowMainScreen;
    }

    private void ShowMainScreen()
    {
        UnityMainThread.umt.AddJob(() => {
            MainScreenView.SetActive(true);

            ChatView.SetActive(false);
            JoinServerView.SetActive(false);
            CreateServerView.SetActive(false);
        });
    }

    private void ShowJoinServer()
    {
        UnityMainThread.umt.AddJob(() =>
        {
            MainScreenView.SetActive(false);
            ChatView.SetActive(false);
            CreateServerView.SetActive(false);

            JoinServerView.SetActive(true);
        });
    }

    private void ShowCreateServer()
    {
        UnityMainThread.umt.AddJob(() => 
        {
            MainScreenView.SetActive(false);
            ChatView.SetActive(false);
            JoinServerView.SetActive(false);

            CreateServerView.SetActive(true);
        });
    }


    private void ShowChat()
    {
        UnityMainThread.umt.AddJob(() =>
        {
            MainScreenView.SetActive(false);
            CreateServerView.SetActive(false);

            ChatView.SetActive(true);
        });
    }

}