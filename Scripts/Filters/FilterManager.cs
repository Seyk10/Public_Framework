using MECS.Core;
using UnityEngine;

namespace MECS.Filters
{
    //* Manager used to collect all the event filter components components
    //* T = IFilterData
    [CreateAssetMenu(fileName = "New_Event_Filter_Manager", menuName = "MECS/Managers/Filter")]
    public class FilterManager : AManager<IFilterData> { }
}