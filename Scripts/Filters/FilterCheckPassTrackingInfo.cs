using System;
using MECS.Entities.Enums;
using UnityEngine;

namespace MECS.Filters
{
    //* Class used to store information of each filter value that has pass checking
    [Serializable]
    public class FilterCheckPassTrackingInfo
    {
        //Editor variables
        [Header("Filter check pass tracking information")]
        public string entityName = null;
        public string entityLayer = null;
        public string entityTag = null;
        public bool isEntityReferenceCheck = false;
        public ScriptableEnum[] scriptableEnums = null;
    }
}