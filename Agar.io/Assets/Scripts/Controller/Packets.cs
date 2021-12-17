using System;

using ProtoBuf;

public enum PacketType
{
    Connection,
    PlayerPosition
}


[ProtoContract]
public class Packet
{
    [ProtoMember(1)]
    public PacketType Type { get; set; }

    //[ProtoInclude(2, typeof(ConnectionPacket))]
    //[ProtoInclude(3, typeof(PlayerPosition))]
    //public class PacketBase { }

    [ProtoMember(2)]
    public PacketBase Message { get; set; }

}

[ProtoContract,
 ProtoInclude(50, typeof(ConnectionPacket)),
 ProtoInclude(51, typeof(PlayerPosition))]
public class PacketBase
{
    [ProtoMember(1)]
    public PacketType Type { get; set; }
}

[ProtoContract]
public class ConnectionPacket : PacketBase
{

    [ProtoMember(2)]
    public string Name { get; set; }

}

[ProtoContract]
public class PlayerPosition : PacketBase
{

    [ProtoMember(2)]
    public string Name { get; set; }

    [ProtoMember(3)]
    public int X { get; set; }

    [ProtoMember(4)]
    public int Y { get; set; }


}
