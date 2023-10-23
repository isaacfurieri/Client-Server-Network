using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;

public class TCPClient : MonoBehaviour
{
    TcpClient client = null;
    public string host;
    public int port;


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
    }

    private void SendData() 
    {
    
    }

    private void ReceiveData()
    {

    }
}
