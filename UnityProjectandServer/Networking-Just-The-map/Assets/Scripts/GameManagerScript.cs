using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public Dictionary<byte , OnHandlePacket> HandlePackets = new Dictionary<byte, OnHandlePacket>(); // <PacketID , OnHandlePacket>
  public delegate void OnHandlePacket(Packet packet);
  public static GameManager instance;

  private void Awake()
  {
        if (instance == null)
        {
          instance = this;
        }
        else if (instance != this)
        {
          Destroy(this);
        }

        AddPacket(((byte)PacketID.S_welcome) , WelcomReceived);
  }

    public void WelcomReceived(Packet packet)
    {
        string message = packet.GetString();
        Debug.Log("Received message from server:" + message);

        int PacketID = packet.GetInt();
        Debug.Log("Received ID from server:" + PacketID);
    }

    public void AddPacket(byte packetID , OnHandlePacket onHandlePacket)
    {
        HandlePackets.Add(packetID , onHandlePacket);
    }

}
