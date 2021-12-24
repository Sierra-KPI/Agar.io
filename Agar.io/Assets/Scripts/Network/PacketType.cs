namespace Agario.Network
{
    public enum PacketType
    {
        ConnectionRequest,
        ConnectionResponse,
        PlayerPosition,
        BoardUpdate,
        PlayerInfo,
        PlayerInfoRequest,
        PlayerInfoResponse,
        LeaderBoardRequest,
        LeaderBoardResponse,
    }
}
