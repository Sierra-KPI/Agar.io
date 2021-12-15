using System;
using System.Collections.Generic;
using System.Numerics;

namespace Agario.Model
{
    public class AgarioGame
    {
        private List<Player> _players;
        private List<Food> _food;
        private const int FoodCount = 10;

        public void Start()
        {
            _players = new List<Player>();
            _food = new List<Food>();

            SpawnFood();
        }

        private void SpawnFood()
        {
            Random random = new Random();

            for (var i = 0; i < FoodCount; i++)
            {
                int x = random.Next(-3,3);
                int y = random.Next(-3,3);
                Vector2 position = new Vector2(x, y);

                Food food = new Food(position);
                _food.Add(food);
            }
        }

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
