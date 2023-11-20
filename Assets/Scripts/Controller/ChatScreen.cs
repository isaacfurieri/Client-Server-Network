using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using TMPro;
using TMPro.EditorUtilities;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ChatScreen : MonoBehaviour
{
    public TMP_InputField TextMessage;
    public TMP_Text ChatText;
    public Transform MessageParent;
    public GameObject ChatTextPrefab;

    public UnityEngine.UI.Button SendButton;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        TCPMyClient.OnClientReceiveMessage += ShowMessage;
        TCPMyServer.OnServerReceiveMessage += ShowMessage;
        SendButton.onClick.AddListener(SendButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SendButtonClicked()
    {
        string inputMessage = TextMessage.text;

        if(!string.IsNullOrWhiteSpace(inputMessage))
        {
            if(!UIController.Instance.IsServer)
            {
                //client
                TCPMyClient.Instance.SendData(inputMessage);
                ShowMessage(inputMessage);
                TextMessage.text = "";
            }
            else
            {
                //server
                TCPMyServer.Instance.SendData(inputMessage);
                ShowMessage(inputMessage);
                TextMessage.text = "";
            }
        }
                
    }

    private void ShowMessage(string msg)
    {
        if (!UIController.Instance.IsServer)
        {
            GameObject chat = Instantiate(ChatTextPrefab, MessageParent);
            chat.GetComponent<TMP_Text>().text = "Server: " + msg;
        }
        else
        {
            GameObject chat = Instantiate(ChatTextPrefab, MessageParent);
            chat.GetComponent<TMP_Text>().text = "Client: " + msg;
        }
    }
}
