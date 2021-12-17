using System;

using ProtoBuf;

public enum PacketType
{
    Connection,
    PlayerPosition
}

[ProtoContract,
 ProtoInclude(50, typeof(ConnectionPacket)),
 ProtoInclude(51, typeof(PlayerPosition))]
public class PacketBase
{
    [ProtoMember(1)]
    public PacketType Type { get; set; }

    //[ProtoMember(2)]
    //public int Id { get; set; }
}

[ProtoContract]
public class ConnectionPacket : PacketBase
{

    [ProtoMember(3)]
    public string Name { get; set; }

}

[ProtoContract]
public class PlayerPosition : PacketBase
{


    [ProtoMember(3)]
    public int X { get; set; }

    [ProtoMember(4)]
    public int Y { get; set; }


}
