using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;

public class Client
{
    public string Id;
    public NetworkStream Stream;
}

public class Message
{
    public string SenderID;
    public string Data;
    public string Time;
    public string Type;
}

public class TCPMyServer : MonoBehaviour
{
    private string host;
    private int port;

    private TcpListener server;
    public static TCPMyServer Instance;

    public static Action OnServerCreated;
    public static Action OnSendMessageServer;
    public static Action<string> OnServerReceiveMessage;

    NetworkStream stream;
    List<Client> clients = new List<Client>();

    private Thread serverThread = null;

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
                stream = client.GetStream();
            }

            Byte[] bytes = new Byte[256];
            int i;
            string msg = "";

            while ((i = stream.Read(bytes, 0, bytes.Length)) > 0)
            {
                msg = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                Debug.LogFormat("Server Received :: {0}", msg);

                if (msg.Contains("UID"))
                {
                    Client c = new Client();
                    Debug.Log( msg.Substring(4, msg.Length - 4));
                    c.Id = msg.Substring(4, msg.Length - 4);
                    c.Stream = stream;
                    clients.Add(c);

                    Debug.Log("Connected user " + c.Id + " and stream is " + c.Stream);

                    foreach (var Client in clients)
                    {
                        Debug.Log("Connected user " + Client.Id + " and stream is " + Client.Stream);
                    }
                }

                foreach (var Client in clients)
                {
                    Debug.Log("Client Loop: " + Client.Id);
                    if (Client.Stream.Equals(stream))
                    {
                        UnityMainThread.umt.AddJob(() => OnServerReceiveMessage?.Invoke(Client.Id + ":" + msg));
                        BroadcastData(Client.Id + ":" + msg);
                    }
                }
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

    public void BroadcastData(string msg)
    {
    foreach (var Client in clients)
    {
        Debug.Log(msg);
        Byte[] bytes = System.Text.Encoding.ASCII.GetBytes(msg);
        Client.Stream.Write(bytes, 0, bytes.Length);
        OnSendMessageServer?.Invoke();

        Debug.Log("Message sent: " + msg + " to client : " + Client.Id);
    }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
