using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class ClientTCP 
{
    private TcpClient m_Socket;
    private NetworkStream m_Stream;

    private byte[] m_Received_Buffer;
    private const int m_Data_Buffer_Size = 4096;
    public void Connect(string ip, int port)
    {
        m_Socket = new TcpClient
        {

            ReceiveBufferSize = m_Data_Buffer_Size,
            SendBufferSize = m_Data_Buffer_Size
        };

        m_Received_Buffer = new byte[m_Data_Buffer_Size];
        
        m_Socket.BeginConnect(ip, port , OnConnectCallback, m_Socket);
      

    }

    private void OnConnectCallback(IAsyncResult result)
    {
        m_Socket.EndConnect(result);

        if (m_Socket.Connected) return; 
     
        m_Stream = m_Socket.GetStream();

        m_Stream.BeginRead(m_Received_Buffer, 0, m_Data_Buffer_Size, OnDataReceivedCallback, null);
    }

    private void OnDataReceivedCallback(IAsyncResult result)
    {
        try
        {
            int bytesRead = m_Stream.EndRead(result);
            if (bytesRead <= 0)
            {
                m_Socket.Close();
                return;
            }

            byte[] data = new byte[bytesRead];
            Array.Copy(m_Received_Buffer, data, bytesRead);

           
            HandlData(data);

            m_Stream.BeginRead(m_Received_Buffer, 0, m_Data_Buffer_Size, OnDataReceivedCallback, null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());

        }
    }

    private void HandlData(byte[] data)
    {
        Packet packet = new Packet(data);
        Byte packetID = packet.GetByte();


    }

}
