using System;
using System.Collections;
using MECS.Tools;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Casting state used for continues detection of entities by physics cast
    public class DetectingCastState : ACastingState
    {
        //events
        public event EventHandler<CastingDetectionArgs> RaycastHitEvent = null;

        //ACastingState, default builder
        public DetectingCastState(MonoBehaviour monoBehaviour, ICastingData data) : base(monoBehaviour, data) { }

        //ACastingState method, run loop process
        public override void RunState()
        {
            //Avoid loop casting if there are missing references necessary for casting type
            bool CanCast()
            {
                bool canCast = true,

                //Check if can cast shapes
                canCastShapes =
                    //Check casting type
                    !data.CastingShape.Equals(EPhysicsCastingShape.Ray)
                    //Check editor references
                    && data.ScaleTransform.CheckEditorReferences();

                //Check if have origin value
                if (NumericTools.IsComparativeCorrect(data.OriginTransforms.Length, 0,
                Conditionals.ENumericConditional.Bigger, " data.OriginTransforms must have at least one value"))
                    canCast = data.OriginTransforms[0].CheckEditorReferences();

                //Check if have direction value
                canCast = data.DirectionTransform.CheckEditorReferences();

                //Check casting shapes if can cast it
                if (canCastShapes && canCast)
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
                            if (NumericTools.IsComparativeCorrect(data.OriginTransforms.Length, 2,
                            Conditionals.ENumericConditional.Equal, " origins on Capsule shape must have 2 values"))

                                //Itinerate origins
                                foreach (TransformReference originReference in data.OriginTransforms)
                                    //Check editor references
                                    if (!originReference.CheckEditorReferences())
                                        canCast = false;
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

                                    //Invoke hit event with casting values
                                    RaycastHitEvent?.Invoke(monoBehaviour,
                                    new CastingDetectionArgs(boxRaycastHit, data, " couldnt notify casting detection"));
                                break;

                            //Check casts with capsule shape
                            case EPhysicsCastingShape.Capsule:
                                if (UnityEngine.Physics.CapsuleCast(data.OriginTransforms[0].Value.position,
                                data.OriginTransforms[1].Value.position,
                                data.ScaleTransform.Value.localScale.magnitude, data.DirectionTransform.Value.position,
                                out RaycastHit capsuleRaycastHit, data.MaxDistance, ~data.IgnoreLayers))

                                    //Invoke hit event with casting values
                                    RaycastHitEvent?.Invoke(monoBehaviour, new CastingDetectionArgs(capsuleRaycastHit, data,
                                    " couldnt notify casting detection"));
                                break;

                            //Check casts with ray shape
                            case EPhysicsCastingShape.Ray:
                                if (UnityEngine.Physics.Raycast(data.OriginTransforms[0].Value.position,
                                 data.DirectionTransform.Value.position, out RaycastHit raycastHit, data.MaxDistance, ~data.IgnoreLayers))

                                    //Invoke hit event with casting values
                                    RaycastHitEvent?.Invoke(monoBehaviour, new CastingDetectionArgs(raycastHit, data,
                                    " couldnt notify casting detection"));
                                break;

                            //Check casts with sphere shape
                            case EPhysicsCastingShape.Sphere:
                                if (UnityEngine.Physics.SphereCast(data.OriginTransforms[0].Value.position,
                                data.ScaleTransform.Value.localScale.magnitude / 2,
                                data.DirectionTransform.Value.position, out RaycastHit sphereRaycastHit, data.MaxDistance, ~data.IgnoreLayers))

                                    //Invoke hit event with casting values
                                    RaycastHitEvent?.Invoke(monoBehaviour, new CastingDetectionArgs(sphereRaycastHit, data,
                                    " couldnt notify casting detection"));
                                break;
                        }

                        yield return new WaitForFixedUpdate();
                    } while (true);
                }

                //Store on data
                data.CastingExecution = monoBehaviour.StartCoroutine(CastingLoop());
            }
        }
    }
}