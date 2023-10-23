using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;

public class TCPServer : MonoBehaviour
{
    public int port;
    public string host;
    private TcpListener server;

    // Start is called before the first frame update
    void Start()
    {
        CreateServer();

    }

    // Create Server
    void CreateServer()
    {
        IPAddress iPAddress = IPAddress.Parse(host);
        server = new TcpListener(iPAddress, port);
        server.Start();

        Debug.Log("Server has been created.");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
