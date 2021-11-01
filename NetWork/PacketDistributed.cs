//This code create by CodeEngine

using System.IO;
using System;
#if UNITY_WP8
 using UnityPortSocket; 
#else
using System.Net.Sockets;
#endif
using Google.ProtocolBuffers;
public abstract class PacketDistributed
{
    //这里根据创建发送给服务器消息的包
    public static PacketDistributed CreatePacket(int packetID)
    {
        PacketDistributed packet = null;
        //根据消息ID 创建消息包
        switch (packetID)
        {
            

        }
        if (null != packet)
        {
            packet.packetID = packetID;
        }
        return packet;
    }
    public void SendPacket()
    {
        NetWorkFrame.GetMe().SendPacket(this);
    }

    public PacketDistributed ParseFrom(byte[] data, int nLen)
    {
        CodedInputStream input = CodedInputStream.CreateInstance(data, 0, nLen);
        PacketDistributed inst = MergeFrom(input, this);
        input.CheckLastTagWas(0);
        return inst;
    }

    public abstract int SerializedSize();
    public abstract void WriteTo(CodedOutputStream data);
    public abstract PacketDistributed MergeFrom(CodedInputStream input, PacketDistributed _Inst);
    public abstract bool IsInitialized();

    //这里根据自己的设计思路消息ID的具体的数据结构
    public int GetPacketID() { return packetID; }
    protected int packetID;
}

