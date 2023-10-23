using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;

public class TCPServer : MonoBehaviour
{
    public string host;
    public int port;

    private TcpListener server;
    private Thread serverThread = null;

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
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
