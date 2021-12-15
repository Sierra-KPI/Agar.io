using System.Collections.Generic;

namespace Agario.Model
{
    public class AgarioGame
    {
        private List<Player> _players;
        private List<Food> _food;

        public List<Player> GetLeaderBoard()
        {
            List<Player> leaderBoard = _players;

            foreach (Player player in _players)
            {
                leaderBoard.Sort((a, b) =>
                {
                    return a.GetTotalRadius().CompareTo(b.GetTotalRadius());
                });
            }

            return leaderBoard;
        }
    }
}
