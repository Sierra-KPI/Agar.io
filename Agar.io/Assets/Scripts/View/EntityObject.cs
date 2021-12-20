using Agario.Model;
using UnityEngine;

namespace Agario.UnityView
{
    public class EntityObject
    {
        public EntityType EntityType;
        public GameObject Prefab;
        public int Number;

        public EntityObject(EntityType entityType,
            GameObject prefab, int number)
        {
            EntityType = entityType;
            Prefab = prefab;
            Number = number;
        }
    }
}
