using System.Collections.Generic;
using Agario.Model;
using UnityEngine;

namespace Agario.UnityView
{
    public class View : MonoBehaviour
    {
        #region Fields

        private readonly Dictionary<int, GameObject> _players = new();
        private readonly List<GameObject> _food = new();
        private EntityFactory _entityFactory = new();

        public static string s_username;

        private static readonly int s_foodNumber = 150;
        private static readonly int s_playerNumber = 10;

        #endregion Fields

        #region Methods

        public void CreatePlayer(Player player)
        {
            GameObject obj = _entityFactory.GetEntity(player);
            _players.Add(player.Id, obj);
        }

        public void CreateEntityObjects(GameObject _foodPrefab,
            GameObject _playerPrefab)
        {
            var _foodObject = new EntityObject(
                EntityType.Food,
                _foodPrefab,
                s_foodNumber
            );
            _entityFactory.AddEntityObject(_foodObject);

            var _playerObject = new EntityObject(
                EntityType.Player,
                _playerPrefab,
                s_playerNumber
            );
            _entityFactory.AddEntityObject(_playerObject);

            _entityFactory.CreateEntityObjects();
        }

        public void ChangePlayersPosition(Dictionary<int, Player> players)
        {

            foreach (Player player in players.Values)
            {
                if (player.Radius == 0)
                {
                    continue;
                }
                if (!_players.ContainsKey(player.Id))
                {
                    CreatePlayer(player);
                }

                Vector3 newPosition = new Vector3(player.Position.X,
                    player.Position.Y, 1);
                _players[player.Id].transform.localPosition = newPosition;

                Vector3 newSize = new Vector3(player.Radius, player.Radius);
                _players[player.Id].transform.localScale = newSize;
            }
        }

        public void ChangeFoodPosition(List<Food> food)
        {
            var foodCount = food.Count;
            var objCount = _food.Count;

            if (foodCount >= objCount)
            {
                for (var j = 0; j < objCount; j++)
                {
                    Vector3 newPosition = new Vector3(food[j].Position.X,
                        food[j].Position.Y, 1);
                    _food[j].transform.localPosition = newPosition;
                }
                for (var j = objCount; j < foodCount; j++)
                {
                    var obj = _entityFactory.GetEntity(food[j]);
                    _food.Add(obj);
                }
            }
            else
            {
                for (var j = 0; j < foodCount; j++)
                {
                    Vector3 newPosition = new Vector3(food[j].Position.X,
                        food[j].Position.Y, 1);
                    _food[j].transform.localPosition = newPosition;
                }
                for (var j = foodCount; j < objCount; j++)
                {
                    Vector3 newPosition = new Vector3(0, 0, 0);
                    _food[j].transform.localPosition = newPosition;
                }
            }
        }

        #endregion Methods
    }
}
