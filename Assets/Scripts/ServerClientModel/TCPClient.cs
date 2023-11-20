using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using System.Threading;


public class TCPClient : MonoBehaviour
{
    NetworkStream stream;
    TcpClient client = null;
    
    private Thread clientThread = null;

    public static TCPClient Instance;
    public static Action OnConnectedToServer;
    public static Action OnSendMessage;

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    ConnectedToServer();
        //}
    }

    public void ConnectedToServer(string hostController, int portController)
    {
        Debug.Log("Request Connection");
        client = new TcpClient(hostController, portController);
        OnConnectedToServer?.Invoke();

        stream = client.GetStream();

        SendData("Hi this is Client");
        
        clientThread = new Thread(ReceiveData);
        clientThread.Start();
    }

    public void SendData(string msg) 
    {
        Debug.Log(msg);
        Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(msg);
        stream.Write(bytes, 0, bytes.Length);
        OnSendMessage?.Invoke();
        Debug.Log("Client sent " + msg);
    }

    public void ReceiveData()
    {
        string msg;

        while (true)
        {
            Byte[] bytes = new Byte[256];
            stream.Read(bytes, 0, bytes.Length);
            msg = System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            Debug.LogFormat("Client Received :: {0}", msg);
        }

    }
}
