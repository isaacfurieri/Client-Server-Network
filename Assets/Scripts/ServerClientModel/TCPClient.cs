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
    
    public string host;
    public int port;
    
    private Thread clientThread = null;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ConnectedToServer();
        }
    }

    private void ConnectedToServer()
    {
        Debug.Log("Request Connection");
        client = new TcpClient(host, port);
        stream = client.GetStream();
        SendData("Hi this is Client");
        
        clientThread = new Thread(ReceiveData);
        clientThread.Start();
    }

    private void SendData(string msg) 
    {
        Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(msg);
        stream.Write(bytes, 0, bytes.Length);
        Debug.Log("Client sent " + msg);
    }

    private void ReceiveData()
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
