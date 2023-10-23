using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;

public class TCPServer : MonoBehaviour
{
    public string host;
    public int port;

    private TcpListener server;
    private Thread serverThread = null;

    NetworkStream stream;

    // Start is called before the first frame update
    void Start()
    {
        CreateServer();

    }

    // Create Server
    void CreateServer()
    {
        serverThread = new Thread(ListenerThread);
        serverThread.Start();
    }

    void ListenerThread()
    {
        string msg;
        
        IPAddress iPAddress = IPAddress.Parse(host);
        
        server = new TcpListener(iPAddress, port);
        server.Start();

        Debug.Log("Server has been created.");

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            if (client != null) 
            {
                Debug.Log("Client is connected : " + client.Client.LocalEndPoint);
            }

            stream = client.GetStream();
            Byte[] bytes = new Byte[256];
            int i;

            while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
            {
                msg = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Debug.LogFormat("Server Received :: {0}", msg);

                string msgToClient = "Hi this is Server";

                bytes = System.Text.Encoding.ASCII.GetBytes(msgToClient);
                stream.Write(bytes, 0, bytes.Length);
                Debug.Log("Server sent :: " + msgToClient);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
