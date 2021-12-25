using System.Collections.Generic;
using Agario.Model;
using UnityEngine;

namespace Agario.UnityView
{
    public class View : MonoBehaviour
    {
        private readonly Dictionary<Entity, GameObject> _entities = new();
        private readonly Dictionary<int, GameObject> _players = new();
        private readonly List<GameObject> _food = new();
        private EntityFactory _entityFactory = new();

        public static string s_username;

        public void CreatePlayer(Player player)
        {
            GameObject obj = _entityFactory.GetEntity(player);
            _entities.Add(player, obj);
            _players.Add(player.Id, obj);
        }

        public void CreateEntityObjects(GameObject _foodPrefab, GameObject _playerPrefab)
        {
            var _foodObject = new EntityObject(
                EntityType.Food,
                _foodPrefab,
                150
            );
            _entityFactory.AddEntityObject(_foodObject);

            var _playerObject = new EntityObject(
                EntityType.Player,
                _playerPrefab,
                10
            );
            _entityFactory.AddEntityObject(_playerObject);

            _entityFactory.CreateEntityObjects();
        }

        public void CreateEntities(List<Entity> Entities)
        {
            foreach (Entity entity in Entities)
            {
                GameObject entityObject = _entityFactory.GetEntity(entity);
                _entities.Add(entity, entityObject);
            }
        }

        public void DestroyEntity(Entity entity)
        {
            var obj = _entities.GetValueOrDefault(entity);
            _entityFactory.ReturnEntity(obj, entity.EntityType);
        }

        public void ChangeGameObjectsPositions()
        {
            foreach (KeyValuePair<Entity, GameObject> keyValue in _entities)
            {
                float xPos = keyValue.Key.Position.X;
                float yPos = keyValue.Key.Position.Y;

                Vector3 newPosition = new Vector3(xPos, yPos);
                keyValue.Value.transform.localPosition = newPosition;
            }
        }

        public void ChangePlayersPosition(Dictionary<int, Player> players)
        {
            foreach (var player in players.Values)
            {
                try
                {
                    if (!_players.ContainsKey(player.Id))
                    {
                        CreatePlayer(player);
                    }
                    Vector3 newPosition = new Vector3(player.Position.X, player.Position.Y);
                    _players[player.Id].transform.localPosition = newPosition;

                    Vector3 newSize = new Vector3(player.Radius, player.Radius);
                    _players[player.Id].transform.localScale = newSize;
                } catch { }
                
            }
            
        }

        public void ChangeFoodPosition(List<Food> food)
        {
            var foodCount = food.Count;
            var objCount = _food.Count;
            if (foodCount >= objCount)
            {
                for (int j = 0; j < objCount; j++)
                {
                    Vector3 newPosition = new Vector3(food[j].Position.X, food[j].Position.Y);
                    _food[j].transform.localPosition = newPosition;
                }
                for (int j = objCount; j < foodCount; j++)
                {
                    try
                    {
                        var obj = _entityFactory.GetEntity(food[j]);
                        _food.Add(obj);
                    } catch { }
                    
                }
            }
            else
            {
                for (int j = 0; j < foodCount; j++)
                {
                    Vector3 newPosition = new Vector3(food[j].Position.X, food[j].Position.Y);
                    _food[j].transform.localPosition = newPosition;
                }
                for (int j = foodCount; j < objCount; j++)
                {
                    //var obj = _food[j];
                    //_food.Remove(obj);
                    //_entityFactory.ReturnEntity(obj, EntityType.Food);
                }
            }


        }



    }
}
