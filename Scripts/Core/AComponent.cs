using UnityEngine;
using static MECS.Tools.DebugTools;
using MECS.LifeCycle;
using MECS.Tools;
using System.Diagnostics;
using MECS.Collections;

namespace MECS.Core
{
    //* Base class for components used on entities, store related data
    //* T = Data type
    //* T2 = Profile type
    public abstract class AComponent<T, T2> : MonoBehaviour where T : AData where T2 : AProfile<T>
    {
        //Editor variables
        [Header("Data")]
        [SerializeField] private T[] data = default;

        //Attributes
        public T[] Data { get => data; set => data = value; }

        //Variables
        protected string entityDebugInformation = null;

        //MonoBehaviour, notify component awakes
        protected virtual void Awake()
        {
            //Set uniq ID
            new SetEntityUniqIDCommand(gameObject).Execute();

            //Store entityDebugInformation
            entityDebugInformation = "Script " + this.GetType().ToString() + " on entity " + transform.gameObject.name;

            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, new StackTrace());

            //Check if data array is safe

            // //Avoid errors based on data reference
            // if (CollectionsTools.arrayTools.IsArrayContentSafe(), new ComplexDebugInformation(basicDebugInformation,
            //     "data reference isn't safe on entity: " + gameObject.name)))
            //     //Execute notification
            //     dataReference.NotifyDataPhase(this, ELifeCyclePhase.Awake,
            //     new ComplexDebugInformation(basicDebugInformation, "couldnt notify component awake"));
        }

        //MonoBehaviour, notify component enables
        protected virtual void OnEnable()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnEnable()");

            // if (ReferenceTools.IsValueSafe(dataReference, new ComplexDebugInformation(basicDebugInformation,
            //     "data reference isn't safe on entity: " + gameObject.name)))
            //     //Execute notification
            //     dataReference.NotifyDataPhase(this, ELifeCyclePhase.Enable,
            //     new ComplexDebugInformation(basicDebugInformation, "couldnt notify component OnEnable"));
        }

        //MonoBehaviour, notify component disables
        protected virtual void OnDisable()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnDisable()");

            // if (ReferenceTools.IsValueSafe(dataReference, new ComplexDebugInformation(basicDebugInformation,
            //     "data reference isn't safe on entity: " + gameObject.name)))
            //     //Execute notification
            //     dataReference.NotifyDataPhase(this, ELifeCyclePhase.Disable,
            //     new ComplexDebugInformation(basicDebugInformation, "couldnt notify component OnDisable"));
        }

        //MonoBehaviour, notify component destroy
        protected virtual void OnDestroy()
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(entityDebugInformation, "OnDestroy()");

            // if (ReferenceTools.IsValueSafe(dataReference, new ComplexDebugInformation(basicDebugInformation,
            //     "data reference isn't safe on entity: " + gameObject.name)))
            //     //Execute notification
            //     dataReference.NotifyDataPhase(this, ELifeCyclePhase.Destroy,
            //     new ComplexDebugInformation(basicDebugInformation, "couldnt notify component OnDestroy"));
        }
    }
}