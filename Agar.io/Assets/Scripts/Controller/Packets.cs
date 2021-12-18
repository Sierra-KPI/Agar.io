﻿using System;

using ProtoBuf;

public enum PacketType
{
    ConnectionRequest,
    ConnectionResponse,
    PlayerPosition,
    BoardUpdate
}

[ProtoContract,
    ProtoInclude(10, typeof(ConnectionRequestPacket)),
    ProtoInclude(11, typeof(ConnectionResponsePacket)),
    ProtoInclude(12, typeof(PlayerPosition)),
    ProtoInclude(13, typeof(BoardUpdatePacket)),]
public class PacketBase
{
    [ProtoMember(1)]
    public PacketType Type { get; set; }

    [ProtoMember(2)]
    public int ClientId { get; set; }

    [ProtoMember(3)]
    public int PacketId { get; set; }
}

[ProtoContract]
public class ConnectionRequestPacket : PacketBase
{

    [ProtoMember(4)]
    public string Name { get; set; }

}

[ProtoContract]
public class ConnectionResponsePacket : PacketBase
{

    [ProtoMember(4)]
    public int ClientPacketId { get; set; }

}

[ProtoContract]
public class PlayerPosition : PacketBase
{



    [ProtoMember(4)]
    public int X { get; set; }

    [ProtoMember(5)]
    public int Y { get; set; }

}

[ProtoContract]
public class BoardUpdatePacket : PacketBase
{

    [ProtoMember(4)]
    public int ClientPacketId { get; set; }

    [ProtoMember(5)]
    public int PlayersNumber { get; set; }

    [ProtoMember(6)]
    public PlayerPosition[] Players { get; set; }

}
