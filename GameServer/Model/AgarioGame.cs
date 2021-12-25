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
        private static readonly Random s_random = new();
        public float Time;
        public bool IsEnded = true;

        public void Start()
        {
            Board = new Board();
            _players = new List<Player>();
            _food = new List<Food>();
            Time = 0;
            IsEnded = false;

            SpawnFood();
        }

        public void Update()
        {
            if (IsEnded) return;
            Time += 1 / 30f;
        }

        private void SpawnFood()
        {
            for (var i = 0; i < FoodCount; i++)
            {
                Food food = new Food(GetRandomPosition());

                _food.Add(food);
                Board.AddEntityToBoard(food);
            }
        }

        public Player AddPlayer()
        {
            Player player = new Player(GetRandomPosition());

            _players.Add(player);
            Board.AddEntityToBoard(player);

            return player;
        }

        private static Vector2 GetRandomPosition()
        {
            int x = s_random.Next(0, 100);
            int y = s_random.Next(0, 100);

            return new Vector2(x, y);
        }

        public List<Player> GetLeaderBoard()
        {
            IsEnded = true;
            List<Player> leaderBoard = _players;
            leaderBoard.Sort((a, b) =>
            {
                return a.Radius.CompareTo(b.Radius);
            });

            return leaderBoard;
        }
    }
}
