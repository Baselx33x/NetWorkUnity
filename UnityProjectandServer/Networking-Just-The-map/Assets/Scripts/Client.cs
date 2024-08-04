using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    public static Client m_Instance; 
    public string m_IPAddress = "127.0.0.1";
    public int m_Port = 25565;
    public int m_ID = 0;


    private ClientTCP m_ClientTCP = new ClientTCP();
    void Awake() 
    {
        if (m_Instance == null)
        {
            m_Instance = this;
          
        }
        else if(m_Instance != this)
        {
            Destroy(this);
        }
    }



    private void Start()
    {
        m_ClientTCP.Connect(m_IPAddress, m_Port);
    }


}
