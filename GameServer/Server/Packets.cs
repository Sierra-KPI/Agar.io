using ProtoBuf;

namespace GameServer
{
    [ProtoContract,
    ProtoInclude(10, typeof(ConnectionRequestPacket)),
    ProtoInclude(11, typeof(ConnectionResponsePacket)),
    ProtoInclude(12, typeof(PlayerPosition)),
    ProtoInclude(13, typeof(BoardUpdatePacket)),
    ProtoInclude(14, typeof(PlayerInfoPacket)),
    ProtoInclude(15, typeof(PlayerInfoRequestPacket)),
    ProtoInclude(16, typeof(PlayerInfoResponsePacket)),
    ProtoInclude(17, typeof(LeaderBoardRequestPacket)),
    ProtoInclude(18, typeof(LeaderBoardResponsePacket)),]
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

        [ProtoMember(5)]
        public string Color { get; set; }
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
        public float X { get; set; }

        [ProtoMember(5)]
        public float Y { get; set; }

        [ProtoMember(6)]
        public float Size { get; set; }
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

    [ProtoContract]
    public class PlayerInfoPacket : PacketBase
    {
        [ProtoMember(4)]
        public string Name { get; set; }

        [ProtoMember(5)]
        public string Color { get; set; }

        [ProtoMember(6)]
        public float Size { get; set; }
    }

    [ProtoContract]
    public class PlayerInfoRequestPacket : PacketBase
    {
        [ProtoMember(4)]
        public int PlayerId { get; set; }
    }

    [ProtoContract]
    public class PlayerInfoResponsePacket : PacketBase
    {
        [ProtoMember(4)]
        public int PlayerId { get; set; }

        [ProtoMember(5)]
        public int ClientPacketId { get; set; }

        [ProtoMember(6)]
        public PlayerInfoPacket Player { get; set; }
    }

    [ProtoContract]
    public class LeaderBoardRequestPacket : PacketBase
    {
        
    }

    [ProtoContract]
    public class LeaderBoardResponsePacket : PacketBase
    {
        [ProtoMember(4)]
        public int ClientPacketId { get; set; }

        [ProtoMember(5)]
        public PlayerInfoPacket[] Players { get; set; }
    }
    
}
