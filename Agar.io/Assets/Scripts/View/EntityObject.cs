using Agario.Model;
using UnityEngine;

namespace Agario.UnityView
{
    public class EntityObject
    {
        #region Fields

        public EntityType EntityType;
        public GameObject Prefab;
        public int Number;

        #endregion Fields

        #region Constructor

        public EntityObject(EntityType entityType,
            GameObject prefab, int number)
        {
            EntityType = entityType;
            Prefab = prefab;
            Number = number;
        }

        #endregion Constuctror
    }
}
