using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PacketID
{
    S_welcome = 1,
    S_spawnPlayer = 2,
    S_playerPosition = 3,
    S_playerRotation = 4,
    S_playerShoot = 5,
    S_playerDisconnected = 6,
    S_playerHealth = 7,
    S_playerDead = 8,
    S_playerRespawned = 9,


    C_spawnPlayer = 126,
    C_welcomeReceived = 127,
    C_playerMovement = 128,
    C_playerShoot = 129,
    C_playerHit = 130,
}
public class Packet {

    List<byte> m_Writeable_Data = new List<byte>();
    byte[] m_Data; 
    int m_Data_Index = 0;
    private int m_Packet_Length = 0;

    public Packet() { }
    public Packet(byte[] data) {
        this.m_Data = data;
        this.m_Data_Index = 0;
        this.m_Packet_Length = GetInt();
    }

    public Byte GetByte()
    {
        Byte b = m_Data[m_Data_Index];
        m_Data_Index+=4; // Look Here why 4 ? Later trye another value to see what happens
        return b;

    }

    public int GetInt()
    {
         // Convert 4 bytes from m_Data starting at index m_Data_Index to a 32-bit integer
        int i = BitConverter.ToInt32(m_Data, m_Data_Index);
        // Increment m_Data_Index by 4
        m_Data_Index += 4;
        // Return the converted integer
        return i;
    }
    public string GetString()
    {
        // Read an integer from the data buffer to determine the length of the string
        int length = GetInt();

        // Decode the bytes in the data buffer starting from m_Data_Index with the determined length into a string
        string s = System.Text.Encoding.ASCII.GetString(m_Data, m_Data_Index, length);

        // Move the data index forward by the length of the extracted string
        m_Data_Index += length;

        // Return the extracted string
        return s;

    }
    public Byte[] GetBytes(int length)
    {
         // Create a new Byte array with the specified length
        Byte[] b = new Byte[length];

        // Copy elements from the m_Data array starting at index m_Data_Index into the new array b
        Array.Copy(m_Data, m_Data_Index, b, 0, length);

        // Increment the m_Data_Index by the length of the copied elements
        m_Data_Index += length;

        // Return the new array b
        return b;
      
    }
    public void AddByte(byte b) {
        m_Writeable_Data.Add(b);
    }

    public void AddBytes(byte[] bytes) {
        m_Writeable_Data.AddRange(bytes);
    }

    public void AddInt(int intdata) {
        AddBytes(BitConverter.GetBytes(intdata));
    }
}
