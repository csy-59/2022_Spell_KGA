using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilityAsset
{
    public class ObjectMove : SingletonBehaviour<ObjectMove>
    {
        public delegate void BeforeService();
        public delegate void AfterService();

        public void ObjectMoveToTargetPosition(Transform objectToMove, Vector3 targetPosition, float Speed)
        {
            StartCoroutine(MoveToPosition(objectToMove, targetPosition, Speed, null, null));
        }
        
        public void ObjectMoveToTargetPosition(Transform objectToMove, Vector3 targetPosition, float Speed,
            in BeforeService beforeService, in AfterService afterService)
        {
            StartCoroutine(MoveToPosition(objectToMove, targetPosition, Speed, beforeService, afterService));
        }

        public IEnumerator MoveToPosition(
            Transform objectToMove, Vector3 targetPosition, float Speed,
            BeforeService beforeService, AfterService afterService)
        {
            beforeService?.Invoke();

            while (true)
            {
                Vector3 newPosition = Vector3.Lerp(objectToMove.position, targetPosition, Speed * Time.deltaTime);

                objectToMove.position = newPosition;

                if ((objectToMove.position - targetPosition).sqrMagnitude < 0.001)
                {
                    objectToMove.position = targetPosition;
                    break;
                }

                yield return null;
            }

            afterService?.Invoke();
        }

        public void ObjectRotateToTargetRotation(Transform objectToMove, Quaternion targetPosition, float Speed)
        {
            StartCoroutine(RotateToRotation(objectToMove, targetPosition, Speed, null, null));
        }

        public void ObjectRotateToTargetRotation(Transform objectToMove, Quaternion targetPosition, float Speed,
            in BeforeService beforeService, in AfterService afterService)
        {
            StartCoroutine(RotateToRotation(objectToMove, targetPosition, Speed, beforeService, afterService));
        }

        public IEnumerator RotateToRotation(
            Transform objectToRotate, Quaternion targetRotation, float Speed,
            BeforeService beforeService, AfterService afterService)
        {
            beforeService?.Invoke();

            while (true)
            {
                Quaternion newRotation = Quaternion.Lerp(objectToRotate.rotation, targetRotation, Speed * Time.deltaTime);
                
                objectToRotate.rotation = newRotation;

                if ((objectToRotate.rotation.eulerAngles - targetRotation.eulerAngles).sqrMagnitude < 0.001)
                {
                    objectToRotate.rotation = targetRotation;
                    break;
                }

                yield return null;
            }

            afterService?.Invoke();
        }
    }

    public class AnimationID
    {
        public static readonly int Bird_Pick = Animator.StringToHash("Pick");
        public static readonly int Bird_Fly = Animator.StringToHash("Fly");

        public static readonly int Bed_Punch = Animator.StringToHash("Punch");
    }
}