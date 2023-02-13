using UnityEngine;
using MECS.Tools;
using MECS.Collections;
using System.Collections.Generic;
using MECS.Entities.Enums;
using MECS.Core;
using MECS.Events;
using MECS.Patrons.Commands;
using static MECS.Tools.DebugTools;
using System.Diagnostics;

namespace MECS.Filters
{
    //* System used to filtrate events using filter conditions on components data
    //* T = IFilterData
    [CreateAssetMenu(fileName = "New_Filter_System", menuName = "MECS/Systems/Filter")]
    public class FilterSystem : ASystem<IFilterData>
    {
        //ASystem, make subscriptions
        protected override void OnEnable()
        {
            //ASystem, base execution
            base.OnEnable();

            NotificationCommand<NotifyFilterSystemArgs>.NotificationEvent += RespondFilterCheckingNotification;
        }

        //ASystem, remove subscriptions
        protected override void OnDisable()
        {
            //ASystem, base execution
            base.OnDisable();

            NotificationCommand<NotifyFilterSystemArgs>.NotificationEvent -= RespondFilterCheckingNotification;
        }

        //ASystem, check if all the values on IFilterData are correct
        protected override bool IsValidData(Component entity, IFilterData data)
        {
            //Return value
            bool isCorrectData = false;

            //Check parameters first
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { entity, data }, " given parameters aren't safe"))
            {
                //Return result            
                isCorrectData =
                    //Check layers reference
                    CollectionsTools.arrayTools.IsArrayContentSafe(data.LayersReference.Value)

                    //Check tags array
                    || CollectionsTools.arrayTools.IsArrayContentSafe(data.TagsReference.Value)

                    //Check scriptable enum array
                    || CollectionsTools.arrayTools.IsArrayContentSafe(data.ScriptableEnums)

                    //Check entity array
                    || CollectionsTools.arrayTools.IsArrayContentSafe(data.EntitiesReference.Value);

                //Debug if data is correct
                if (!isCorrectData)
                    new NotificationCommand<DebugArgs>(entity,
                    new DebugArgs(" filter data must have one valid array on entity: " + entity.gameObject.name,
                    LogType.Error, new StackTrace(true)))
                    .Execute();
            }

            return isCorrectData;
        }

        //ASystem, used to check filter data values and notify if are correct
        private void RespondFilterCheckingNotification(object sender, NotifyFilterSystemArgs args)
        {
            //Check if parameters are valid
            if (ReferenceTools.AreEventParametersValid(sender, args, " given parameters on filter checking aren't valid"))
            {
                //Entity references
                GameObject entityToCheck = args.entityToCheck;
                //Try to get component
                entityToCheck.TryGetComponent<ScriptableEnumComponent>(out ScriptableEnumComponent component);
                ScriptableEnumComponent enumComponent = component ? component : null;

                //Data that has pass the filter
                List<IFilterData> passData = new();

                //Itinerate data and check filters
                foreach (IFilterData data in args.iFilterDataArray)
                {
                    //Local method, check filters
                    bool CanInvoke(int[] layers, string[] tags, ScriptableEnum[] scriptableEnums)
                    {
                        bool canInvoke = false;

                        //Local method
                        //Itinerate data scriptable enums and check with entity enums
                        bool HasEntityEnum(ScriptableEnum[] scriptableEnums)
                        {
                            bool returnValue = false;

                            //Avoid missing component
                            if (ReferenceTools.IsValueSafe(enumComponent))
                                //Avoid missing references
                                if (CollectionsTools.arrayTools.IsArrayContentSafe(enumComponent.Data,
                                args.debugMessage + " given enum data array inst safe"))
                                    //Itinerate component data
                                    foreach (var enumData in enumComponent.Data)
                                        //Check if its the correct enum
                                        if (CollectionsTools.arrayTools.HasArrayObject(scriptableEnums, enumData.ScriptableEnum))
                                        {
                                            returnValue = true;
                                            break;
                                        }

                            return returnValue;
                        }

                        //Check filters
                        //Check layers
                        bool passLayerFilter = layers.Length == 0 || CollectionsTools
                        .arrayTools.HasArrayObject(layers, entityToCheck.layer),
                        //Check tags
                        passTagFilter = tags.Length == 0 || CollectionsTools
                        .arrayTools.HasArrayObject(tags, entityToCheck.tag),
                        //Check entities references
                        passEntityFilter = data.EntitiesReference.Value.Length == 0 || CollectionsTools
                        .arrayTools.HasArrayObject(data.EntitiesReference.Value, entityToCheck),
                        //Check enums
                        passEnumFilter = scriptableEnums.Length == 0 || HasEntityEnum(scriptableEnums);

                        canInvoke = passLayerFilter && passTagFilter && passEntityFilter && passEnumFilter;

                        //Debug information if its necessary
                        if (MECSSettings.CanDebug && canInvoke)
                        {
                            //Create new debug information
                            FilterCheckPassTrackingInfo filterCheckPassTrackingInfo = new();

                            filterCheckPassTrackingInfo.entityName = entityToCheck.name;

                            //Set values
                            //Set layer value
                            if (passLayerFilter)
                                filterCheckPassTrackingInfo.entityLayer = entityToCheck.layer.ToString();

                            //Set tag value
                            if (passTagFilter)
                                filterCheckPassTrackingInfo.entityTag = entityToCheck.tag;

                            //Set entity value
                            if (passEntityFilter)
                                filterCheckPassTrackingInfo.isEntityReferenceCheck = true;

                            //Set enum value
                            if (passEnumFilter)
                                filterCheckPassTrackingInfo.scriptableEnums = scriptableEnums;

                            //Check data tracking current information
                            if (ReferenceTools.IsValueSafe(data.FilterCheckingPassesTrackingInformation))
                            {
                                //Add value to array
                                if (CollectionsTools.arrayTools.AddValue(data.FilterCheckingPassesTrackingInformation,
                                filterCheckPassTrackingInfo, out FilterCheckPassTrackingInfo[] trackingArray,
                                args.debugMessage + " couldnt add value to tracking array"))
                                    //Set new array on data
                                    data.FilterCheckingPassesTrackingInformation = trackingArray;
                            }
                            //Create array and add value
                            else
                                data.FilterCheckingPassesTrackingInformation = new FilterCheckPassTrackingInfo[]
                                { filterCheckPassTrackingInfo };
                        }

                        return canInvoke;
                    }

                    //Try to convert sender to component type
                    if (TypeTools.ConvertToType<Component>(sender, out Component entityComponent,
                    args.debugMessage + " couldnt convert sender to component"))
                        //Check if given data is valid
                        if (IsValidData(entityComponent, data))
                            //Check if can invoke and add data to pass list
                            if (CanInvoke(data.LayersReference.Value, data.TagsReference.Value, data.ScriptableEnums))
                                passData.Add(data);
                }

                //Check if can invoke
                if (passData.Count > 0)
                {
                    //Store all the events to notify
                    List<IEventData> iEventDataList = new();

                    //Itinerate valid filters
                    foreach (IFilterData data in passData)
                        //Convert data to event data 
                        if (TypeTools.ConvertToType(data, out IEventData eventData,
                        args.debugMessage + " filter data doesnt implement IEventData interface"))
                            iEventDataList.Add(eventData);

                    //Notify event system
                    new NotificationCommand<NotifyEventsInvocationArgs>(sender,
                        new NotifyEventsInvocationArgs(iEventDataList.ToArray(),
                        args.debugMessage + " couldnt raise events from given event data"))
                        .Execute();
                }
            }
        }
    }
}