using System;
using System.Collections.Generic;
using System.Numerics;

namespace Agario.Model
{
    public class AgarioGame
    {
        #region Fields

        private List<Player> _players;
        private List<Food> _food;
        public Board Board;
        private const int FoodCount = 100;
        private static readonly Random s_random = new();
        public float Time;
        public bool IsEnded = true;

        #endregion Fields

        #region Methods

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
            if (IsEnded)
            {
                return;
            }

            Time += 1 / 30f;

            CheckCollisions();
        }

        private void CheckCollisions()
        {
            List<Entity> allEntities = GetAllEntities();

            for (var i = 0; i < allEntities.Count - 1; i++)
            {
                for (var j = i + 1; j < allEntities.Count; j++)
                {
                    Entity firstEntity = allEntities[i];
                    Entity secondEntity = allEntities[j];

                    if (CollisionDetection.AreColliding(firstEntity,
                        secondEntity))
                    {
                        if (firstEntity.Radius > secondEntity.Radius &&
                            firstEntity.EntityType == EntityType.Player &&
                            !firstEntity.IsDead && !secondEntity.IsDead)
                        {
                            Player player = (Player)firstEntity;
                            player.Kill(secondEntity);
                            Board.RemoveEntityFromBoard(secondEntity);
                        }
                    }
                }
            }
        }

        private List<Entity> GetAllEntities()
        {
            List<Entity> allEntities = new List<Entity>();

            allEntities.AddRange(_players);
            allEntities.AddRange(_food);

            return allEntities;
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

        public void RemovePlayer(Player player)
        {
            _players.Remove(player);

            Board.RemoveEntityFromBoard(player);
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
                return b.Radius.CompareTo(a.Radius);
            });

            return leaderBoard;
        }

        #endregion Methods
    }
}
