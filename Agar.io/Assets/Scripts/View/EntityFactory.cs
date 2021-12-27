using System.Collections.Generic;
using Agario.Model;
using UnityEngine;

namespace Agario.UnityView
{
    public class EntityFactory : MonoBehaviour
    {
        #region Fields

        public GameObject PlayerPrefab;
        public List<EntityObject> EntityObjects = new();
        public Dictionary<EntityType,
            Queue<GameObject>> EntityDictionary = new();

        public static EntityFactory Instance;

        private const string NoEntityMessage = "No such entity: ";

        private Color _foodColor = new(0, 0, 0);
        private const float PlayerColorMaxRange = 0.8f;

        #endregion Fields

        #region Constructor

        public EntityFactory()
        {
            Instance = this;
        }

        #endregion Constructor

        #region Methods

        public void AddEntityObject(EntityObject entityObject)
        {
            EntityObjects.Add(entityObject);
        }

        public void CreateEntityObjects()
        {
            foreach (EntityObject entityObject in EntityObjects)
            {
                if (EntityDictionary.ContainsKey(entityObject.EntityType))
                {
                    break;
                }

                Queue<GameObject> objectQueue = new Queue<GameObject>();

                for (var i = 0; i < entityObject.Number; i++)
                {
                    GameObject obj = Instantiate(entityObject.Prefab);
                    
                    if (entityObject.EntityType == EntityType.Food)
                    {
                        SetFoodColor(obj);
                    }
                    else
                    {
                        SetRandomColor(obj);
                    }
                    
                    obj.SetActive(false);
                    objectQueue.Enqueue(obj);
                }

                EntityDictionary.Add(entityObject.EntityType, objectQueue);
            }
        }

        public GameObject GetEntity(Entity entity)
        {
            if (!EntityDictionary.ContainsKey(entity.EntityType))
            {
                Debug.LogWarning(NoEntityMessage + entity.EntityType);

                return null;
            }

            GameObject entityObject =
                EntityDictionary[entity.EntityType].Dequeue();
            entityObject.SetActive(true);

            float xPosition = entity.Position.X;
            float yPosition = entity.Position.Y;
            entityObject.transform.localPosition =
                new Vector3(xPosition, yPosition);

            entityObject.transform.localScale =
                new Vector3(entity.Radius, entity.Radius);

            return entityObject;
        }

        private void SetRandomColor(GameObject entityObject)
        {
            SpriteRenderer spriteRenderer =
                entityObject.GetComponent<SpriteRenderer>();

            Color playerColor = new Color(
                Random.Range(0f, PlayerColorMaxRange),
                Random.Range(0f, PlayerColorMaxRange),
                Random.Range(0f, PlayerColorMaxRange)
            );

            spriteRenderer.color = playerColor;
        }

        private void SetFoodColor(GameObject entityObject)
        {
            SpriteRenderer spriteRenderer =
                entityObject.GetComponent<SpriteRenderer>();

            spriteRenderer.color = _foodColor;
        }

        public void ReturnEntity(GameObject entityObject,
            EntityType entityType)
        {
            entityObject.SetActive(false);

            EntityDictionary[entityType].Enqueue(entityObject);
        }

        #endregion Methods
    }
}
