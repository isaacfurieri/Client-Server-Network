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
    public TMP_Text ChatText;

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
        string inputMessage = TextMessage.text;

        if(!string.IsNullOrWhiteSpace(inputMessage))
        {
            if(!UIController.Instance.IsServer)
            {
                //client
                TCPMyClient.Instance.SendData(inputMessage);
                Debug.Log("message sent from client: " + inputMessage);
                TextMessage.text = "";
            }
            else
            {
                //server
                TCPMyServer.Instance.SendData(inputMessage);
                Debug.Log("message sent from server: " + inputMessage);
                TextMessage.text = "";
            }
        }
                
    }
}
