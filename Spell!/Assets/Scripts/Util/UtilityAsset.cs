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
        
        public void ObjectMoveToTargetPosition(Transform objectToMove, Vector3 targetPosition, 
            float Speed, GameObject layerChangeObject)
        {
            StartCoroutine(MoveToPosition(
                objectToMove, targetPosition, Speed,
                new BeforeService(() => { layerChangeObject.layer = LayerMask.NameToLayer("Default"); }),
                new AfterService(()=> { layerChangeObject.layer = LayerMask.NameToLayer("Interactive"); })
                ));
        }
        
        public void ObjectMoveToTargetPosition(Transform objectToMove, Vector3 targetPosition, float Speed,
            in BeforeService beforeService, in AfterService afterService)
        {
            StartCoroutine(MoveToPosition(objectToMove, targetPosition, Speed, beforeService, afterService));
        }

        private IEnumerator MoveToPosition(
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

        public void ObjectRotateToTargetRotation(Transform objectToMove, Quaternion targetRotation, float Speed)
        {
            StartCoroutine(RotateToRotation(objectToMove, targetRotation, Speed, null, null));
        }
        public void ObjectRotateToTargetRotation(Transform objectToMove, Quaternion targetRotation,
            float Speed, GameObject layerChangeObject)
        {
            StartCoroutine(RotateToRotation(
                objectToMove, targetRotation, Speed,
                new BeforeService(() => { layerChangeObject.layer = LayerMask.NameToLayer("Default"); }),
                new AfterService(() => { layerChangeObject.layer = LayerMask.NameToLayer("Interactive"); })
                ));
        }

        public void ObjectRotateToTargetRotation(Transform objectToMove, Quaternion targetPosition, float Speed,
            in BeforeService beforeService, in AfterService afterService)
        {
            StartCoroutine(RotateToRotation(objectToMove, targetPosition, Speed, beforeService, afterService));
        }

        private IEnumerator RotateToRotation(
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

    public static class PlayerPrefsKey
    {
        public const string EndingListKey = "Ending";

        // 초기화 값
        private static readonly Dictionary<string, int> ResetValue = new Dictionary<string, int> {
            { EndingListKey, 0}
        };
        private static bool hasBeenReset = false;

        // 초기화
        private static void ResetAllPrefs(bool resetAgain)
        {
            if (resetAgain)
            {
                PlayerPrefs.DeleteAll();

                foreach (KeyValuePair<string, int> reset in ResetValue)
                {
                    PlayerPrefs.SetInt(reset.Key, reset.Value);
                }

                hasBeenReset = true;
            }
        }

        public static void SetEndingList(int endingNumber)
        {
            if(!hasBeenReset)
            {
                ResetAllPrefs(false);
            }

            int preEndingList = PlayerPrefs.GetInt(EndingListKey);
            int newEndingList = preEndingList | (1 << endingNumber);
            PlayerPrefs.SetInt(EndingListKey, newEndingList);
        }

        public static bool IsEndingCollacted(int endingNumber)
        {
            if (!hasBeenReset)
            {
                ResetAllPrefs(false);
            }

            int endingList = PlayerPrefs.GetInt(EndingListKey);
            return ((endingList & (1 << endingNumber)) != 0);
        }
    }

}