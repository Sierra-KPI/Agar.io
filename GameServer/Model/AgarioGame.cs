using System;
using System.Collections.Generic;
using System.Numerics;

namespace Agario.Model
{
    public class AgarioGame
    {
        private List<Player> _players;
        private List<Food> _food;
        public Board Board;
        private const int FoodCount = 10;

        public void Start()
        {
            Board = new Board();
            _players = new List<Player>();
            _food = new List<Food>();

            SpawnFood();
        }

        private void SpawnFood()
        {
            Random random = new Random();

            for (var i = 0; i < FoodCount; i++)
            {
                int x = random.Next(0, 100);
                int y = random.Next(0, 100);
                Vector2 position = new Vector2(x, y);

                Food food = new Food(position);
                _food.Add(food);
                Board.AddEntityToBoard(food);
            }
        }

        public Player AddPlayer()
        {
            Random random = new Random();
            int x = random.Next(0, 100);
            int y = random.Next(0, 100);
            Vector2 position = new Vector2(x, y);

            Player player = new Player(position);
            _players.Add(player);

            return player;
        }

        public List<Player> GetLeaderBoard()
        {
            List<Player> leaderBoard = _players;

            foreach (Player player in _players)
            {
                leaderBoard.Sort((a, b) =>
                {
                    return a.Radius.CompareTo(b.Radius);
                });
            }

            return leaderBoard;
        }
    }
}
