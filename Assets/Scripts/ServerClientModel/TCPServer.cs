using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;

public class TCPServer : MonoBehaviour
{
    private string host;
    private int port;

    private TcpListener server;
    private Thread serverThread = null;

    public static TCPServer Instance;
    
    public static Action OnServerCreated;
    public static Action OnSendMessageServer;
    public static Action OnReceive;

    NetworkStream stream;

    //Awake is called before the game starts
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //CreateServer(string, int);
    }

    // Create Server
    public void CreateServer(string hostController, int portController)
    {
        //Assign value from Controller
        host = hostController;
        port = portController;

        //Create server logic
        serverThread = new Thread(ListenerThread);
        serverThread.Start();
        /*   
           if (OnServerCreated != null)
               OnServerCreated.Invoke();

       The code above is equal the line bellow
       */

    }

    void ListenerThread()
    {
        string msg;
        
        IPAddress iPAddress = IPAddress.Parse(host);
        
        server = new TcpListener(iPAddress, port);
        server.Start();

        Debug.Log("Server has been created.");
        OnServerCreated?.Invoke();

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

                SendData(msgToClient);
            }
        }
    }
    public void SendData(string msg)
    {
        Debug.Log(msg);
        Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(msg);
        stream.Write(bytes, 0, bytes.Length);
        OnSendMessageServer?.Invoke();
        Debug.Log("Server sent " + msg);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
