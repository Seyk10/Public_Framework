using MECS.Entities.Enums;
using MECS.Variables.References;

namespace MECS.Filters
{
    //* Interface used to set the variables used on an filter data type structure
    public interface IFilterData
    {
        //Variables       
        public IntArrayReference LayersReference { get; }
        public StringArrayReference TagsReference { get; }
        public ScriptableEnum[] ScriptableEnums { get; }
        public GameObjectArrayReference EntitiesReference { get; }
        public FilterCheckPassTrackingInfo[] FilterCheckingPassesTrackingInformation { get; set; }
    }
}