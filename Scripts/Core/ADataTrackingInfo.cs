using System;
using UnityEngine;

namespace MECS.Core
{
    //* Abstract class used to store information on managers related to entities
    [Serializable]
    public abstract class ADataTrackingInfo
    {
        //Editor variables
        [Header("Tracking information")]
        [SerializeReference] private string entityName = null;

        //Default builder
        public ADataTrackingInfo(string entityName) => this.entityName = entityName;
    }
}