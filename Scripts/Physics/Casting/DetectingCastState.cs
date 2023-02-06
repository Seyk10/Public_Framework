using System;
using System.Collections;
using MECS.Variables.References;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Physics.Casting
{
    //* Casting state used for continues detection of entities by physics cast
    public class DetectingCastState : ACastingState
    {
        //events
        public event EventHandler<CastingDetectionArgs> RaycastHitEvent = null;

        //ACastingState, default builder
        public DetectingCastState(MonoBehaviour monoBehaviour, ICastingData data, ComplexDebugInformation complexDebugInformation)
        : base(monoBehaviour, data, complexDebugInformation) { }

        //ACastingState, run loop process
        public override void RunState()
        {
            //Avoid loop casting if there are missing references necessary for casting type
            bool CanCast()
            {
                bool canCast = true;

                //Values used on basic shapes castings
                bool CanCastShapes()
                {
                    bool canCastShapes = true;

                    //Check if have scale value
                    if (data.CastingShape != EPhysicsCastingShape.Ray && !data.ScaleTransform.CheckEditorReferences())
                    {
                        canCastShapes = false;
#if UNITY_EDITOR
                        Debug.LogWarning("Warning: Its necessary a scale transform on casting shapes.");
#endif
                    }

                    return canCastShapes;
                }

                //Check if have origin value
                if (data.OriginTransforms.Length > 0)
                    if (!data.OriginTransforms[0].CheckEditorReferences())
                    {
                        canCast = false;
#if UNITY_EDITOR
                        Debug.LogWarning("Warning: Its necessary a origin transform on casting shapes.");
#endif
                    }

                //Check if have direction value
                if (!data.DirectionTransform.CheckEditorReferences())
                {
                    canCast = false;
#if UNITY_EDITOR
                    Debug.LogWarning("Warning: Its necessary a direction transform on casting shapes.");
#endif
                }

                //Check shapes
                CanCastShapes();

                //Check casting type
                switch (data.CastingShape)
                {
                    case EPhysicsCastingShape.Box:
                        canCast = data.ScaleTransform.CheckEditorReferences()
                        && data.DirectionTransform.CheckEditorReferences()
                        && data.OrientationQuaternion.CheckEditorReferences();
                        break;

                    case EPhysicsCastingShape.Capsule:
                        //Check if origin has 2 values
                        if (data.OriginTransforms.Length == 2)
                        {
                            //Itinerate origins
                            foreach (TransformReference originReference in data.OriginTransforms)
                            {
                                //Check editor references
                                if (!originReference.CheckEditorReferences())
                                    canCast = false;
                            }
                        }
#if UNITY_EDITOR
                        else Debug.LogWarning("Warning: Origins on Capsule shape must have 2 values");
#endif
                        break;
                }

                return canCast;
            }

            //Avoid errors
            if (CanCast())
            {
                //Local method, run casting loop
                IEnumerator CastingLoop()
                {
                    //Loop the casting process
                    do
                    {
                        //Cast different shapes
                        switch (data.CastingShape)
                        {
                            //Check casts with box shape
                            case EPhysicsCastingShape.Box:
                                if (UnityEngine.Physics.BoxCast(data.OriginTransforms[0].Value.position,
                                data.ScaleTransform.Value.localScale, data.DirectionTransform.Value.position,
                                out RaycastHit boxRaycastHit, data.OrientationQuaternion.Value, data.MaxDistance, ~data.IgnoreLayers))
                                    RaycastHitEvent?.Invoke(monoBehaviour,
                                    new CastingDetectionArgs(boxRaycastHit, data,
                                    complexDebugInformation.AddTempCustomText("couldnt notify casting detection")));
                                break;

                            //Check casts with capsule shape
                            case EPhysicsCastingShape.Capsule:
                                if (UnityEngine.Physics.CapsuleCast(data.OriginTransforms[0].Value.position,
                                data.OriginTransforms[1].Value.position,
                                data.ScaleTransform.Value.localScale.magnitude, data.DirectionTransform.Value.position,
                                out RaycastHit capsuleRaycastHit, data.MaxDistance, ~data.IgnoreLayers))
                                    RaycastHitEvent?.Invoke(monoBehaviour, new CastingDetectionArgs(capsuleRaycastHit, data,
                                    complexDebugInformation.AddTempCustomText("couldnt notify casting detection")));
                                break;

                            //Check casts with ray shape
                            case EPhysicsCastingShape.Ray:
                                if (UnityEngine.Physics.Raycast(data.OriginTransforms[0].Value.position,
                                 data.DirectionTransform.Value.position, out RaycastHit raycastHit, data.MaxDistance, ~data.IgnoreLayers))
                                    RaycastHitEvent?.Invoke(monoBehaviour, new CastingDetectionArgs(raycastHit, data,
                                    complexDebugInformation.AddTempCustomText("couldnt notify casting detection")));
                                break;

                            //Check casts with sphere shape
                            case EPhysicsCastingShape.Sphere:
                                if (UnityEngine.Physics.SphereCast(data.OriginTransforms[0].Value.position,
                                data.ScaleTransform.Value.localScale.magnitude / 2,
                                data.DirectionTransform.Value.position, out RaycastHit sphereRaycastHit, data.MaxDistance, ~data.IgnoreLayers))
                                    RaycastHitEvent?.Invoke(monoBehaviour, new CastingDetectionArgs(sphereRaycastHit, data,
                                    complexDebugInformation.AddTempCustomText("couldnt notify casting detection")));
                                break;
                        }

                        yield return new WaitForFixedUpdate();
                    } while (true);
                }

                //Store on data
                data.CastingExecution = monoBehaviour.StartCoroutine(CastingLoop());
            }
#if UNITY_EDITOR
            else Debug.LogWarning("Warning: There aren't the necessary values to target casting shape.");
#endif
        }
    }
}