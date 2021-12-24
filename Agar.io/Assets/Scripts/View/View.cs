using System.Collections.Generic;
using Agario.Model;
using UnityEngine;

namespace Agario.UnityView
{
    public class View : MonoBehaviour
    {
        private readonly Dictionary<Entity, GameObject> _entities = new();
        private EntityFactory _entityFactory = new();

        public static string s_username;

        public void CreatePlayer(Entity player)
        {
            GameObject obj = _entityFactory.GetEntity(player);
            _entities.Add(player, obj);
        }

        public void CreateEntityObjects(GameObject _foodPrefab)
        {
            var _foodObject = new EntityObject(
                EntityType.Food,
                _foodPrefab,
                10
            );
            _entityFactory.AddEntityObject(_foodObject);

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
    }
}
