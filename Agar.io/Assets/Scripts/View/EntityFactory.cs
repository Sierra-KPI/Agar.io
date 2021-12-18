using System.Collections.Generic;
using Agario.Model;
using UnityEngine;

namespace Agario.UnityView
{
    public class EntityFactory : MonoBehaviour
    {
        public static int BlobsNumber = 200;
        public static int FoodNumber = 200;
        public GameObject blobPrefab;
        public List<EntityObject> EntityObjects = new();
        public Dictionary<EntityType,
            Queue<GameObject>> EntityDictionary = new();

        public static EntityFactory Instance;

        public EntityFactory()
        {
            Instance = this;
        }

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
                    obj.SetActive(false);
                    objectQueue.Enqueue(obj);
                }
                EntityDictionary.Add(entityObject.EntityType, objectQueue);
            }
        }

        public GameObject GetEntityFood(Food entity)
        {
            if (!EntityDictionary.ContainsKey(entity.EntityType))
            {
                Debug.LogWarning("No such entity: " + entity.EntityType);
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

            entityObject.GetComponent<Renderer>().material.color =
                new Color(1, 1, 1);

            return entityObject;
        }

        public void ReturnEntity(GameObject entityObject,
            EntityType entityType)
        {
            entityObject.SetActive(false);
            EntityDictionary[entityType].Enqueue(entityObject);
        }
    }
}
