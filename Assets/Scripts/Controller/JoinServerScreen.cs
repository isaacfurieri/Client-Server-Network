using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class JoinServerScreen : MonoBehaviour
{
    public static JoinServerScreen Instance;

    public TMP_InputField Username;
    public TMP_InputField IPAddress;
    public TMP_InputField Port;

    public Button JoinServerButton;
    public Button BackButton;

    public static Action onBackButtonClicked;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        JoinServerButton.onClick.AddListener(JoinServerButtonClicked);
        BackButton.onClick.AddListener(BackButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void JoinServerButtonClicked()
    {
        Debug.Log("Server CLicked");
        TCPMyClient.Instance.ConnectedToServer("UID:" + Username.text, IPAddress.text, int.Parse(Port.text));
    }

    void BackButtonClicked()
    {
        onBackButtonClicked?.Invoke();
    }
}
