namespace Agario.Network
{
    public enum PacketType
    {
        ConnectionRequest,
        ConnectionResponse,
        PlayerPosition,
        BoardUpdate,
        PlayerInfoRequest,
        PlayerInfoResponse,
        LeaderBoardRequest,
        LeaderBoardResponse,
    }
}
