using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChatScreen : MonoBehaviour
{
    public TMP_InputField TextMessage;
    public ScrollView ScrollMessages;

    public UnityEngine.UI.Button SendButton;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        SendButton.onClick.AddListener(SendButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SendButtonClicked()
    {
        TCPClient.Instance.SendData(TextMessage.GetComponent<TMP_InputField>().text);
        TextMessage.GetComponent<TMP_InputField>().text = string.Empty;
        Debug.Log("Send message clicked");
    }
}
