using MECS.Core;
using MECS.Entities.Enums;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Filters
{
    //* Tracking information for IFilterData
    public class FilterTrackingInfo : ADataTrackingInfo, IFilterData
    {
        //Editor variables
        [Header("Filter tracking info")]
        [SerializeReference] private IntArrayReference layersReference = null;
        [SerializeReference] private StringArrayReference tagsReference = null;
        [SerializeReference] private ScriptableEnum[] scriptableEnums = null;
        [SerializeReference] private GameObjectArrayReference entitiesReference = null;
        [SerializeReference] private FilterCheckPassTrackingInfo[] filterCheckingPassesTrackingInformation = null;

        //Attributes
        public IntArrayReference LayersReference => layersReference;
        public StringArrayReference TagsReference => tagsReference;
        public ScriptableEnum[] ScriptableEnums => scriptableEnums;
        public GameObjectArrayReference EntitiesReference => entitiesReference;
        public FilterCheckPassTrackingInfo[] FilterCheckingPassesTrackingInformation
        { get => filterCheckingPassesTrackingInformation; set => filterCheckingPassesTrackingInformation = value; }

        //Base builder
        public FilterTrackingInfo(string entityName, IntArrayReference layersReference, StringArrayReference tagsReference,
        ScriptableEnum[] scriptableEnums, GameObjectArrayReference entitiesReference,
        FilterCheckPassTrackingInfo[] filterCheckingPassesTrackingInformation) : base(entityName)
        {
            this.layersReference = layersReference;
            this.tagsReference = tagsReference;
            this.scriptableEnums = scriptableEnums;
            this.entitiesReference = entitiesReference;
            FilterCheckingPassesTrackingInformation = filterCheckingPassesTrackingInformation;
            FilterCheckingPassesTrackingInformation = filterCheckingPassesTrackingInformation;
        }

    }
}