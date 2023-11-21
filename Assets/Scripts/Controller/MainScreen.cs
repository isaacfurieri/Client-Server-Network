using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    public static MainScreen Instance;

    public Button ServerButton;
    public Button ClientButton;

    public static Action onServerButtonClicked;
    public static Action onClientButtonClicked;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ServerButton.onClick.AddListener(ServerButtonClicked);
        ClientButton.onClick.AddListener(ClientButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ServerButtonClicked()
    {
        Debug.Log("Server Screen CLicked");
        onServerButtonClicked?.Invoke();
    }

    void ClientButtonClicked()
    {
        onClientButtonClicked?.Invoke();
        Debug.Log("Client CLicked");
    }
}