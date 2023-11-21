using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateServerScreen : MonoBehaviour
{
    public static CreateServerScreen Instance;

    public TMP_InputField IPAddress;
    public TMP_InputField Port;

    public Button ServerButton;
    public Button BackButton;

    public static Action onBackButtonClicked;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        ServerButton.onClick.AddListener(ServerButtonClicked);
        BackButton.onClick.AddListener(BackButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ServerButtonClicked()
    {
        Debug.Log("Server CLicked");
        TCPMyServer.Instance.CreateServer(IPAddress.text, int.Parse(Port.text));
    }

    void BackButtonClicked()
    {
        onBackButtonClicked?.Invoke();
    }
}