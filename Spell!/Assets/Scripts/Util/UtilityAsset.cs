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
        public const string ScrollKey = "Scroll";
        public const string CristalKey = "Cristal";
        public const string MagicMaterialKey = "MagicMaterial";
        public const string CommonItemKey = "CommonItem";

        // 초기화 값
        private static readonly Dictionary<string, int> ResetValue = new Dictionary<string, int> {
            { EndingListKey, 0},
            { ScrollKey, 0 },
            { CristalKey, 0 },
            { MagicMaterialKey, 0 },
            { CommonItemKey, 0 }
        };
        private static bool hasBeenReset = false;

        // 초기화
        private static void ResetAllPrefs(bool resetAgain)
        { 
            if(!hasBeenReset || resetAgain)
            {
                PlayerPrefs.DeleteAll();

                foreach (KeyValuePair<string, int> reset in ResetValue)
                {
                    PlayerPrefs.SetInt(reset.Key, reset.Value);
                }

                hasBeenReset = true;
            }
        }

        private static void SetListValue(string key, int number)
        {
            ResetAllPrefs(false);

            int preList = PlayerPrefs.GetInt(key);
            int newList = preList | (1 << number);
            PlayerPrefs.SetInt(key, newList);
        }

        public static void SetEndingList(int endingNumber)
        {
            SetListValue(EndingListKey, endingNumber);
        }

        public static void SetScrollList(int scrollNumber)
        {
            SetListValue(ScrollKey, scrollNumber);
        }

        public static void SetCristalList(int cristalNumber)
        {
            SetListValue(CristalKey, cristalNumber);
        }

        public static void SetMagicalMaterialList(int materialNumber)
        {
            SetListValue(MagicMaterialKey, materialNumber);
        }

        public static void SetCommonItemList(int commonItemNumber)
        {
            SetListValue(CommonItemKey, commonItemNumber);
        }

        public static bool GetListValueInBool(string key, int number)
        {
            ResetAllPrefs(false);

            int endingList = PlayerPrefs.GetInt(key);
            return ((endingList & (1 << number)) != 0);
        }

        public static bool IsEndingCollacted(int endingNumber)
        {
            return GetListValueInBool(EndingListKey, endingNumber);
        }

        public static bool isScrollCollacted(int scrollNumber)
        {
            return GetListValueInBool(ScrollKey, scrollNumber);
        }
        public static bool isCristalCollacted(int cristalNumber)
        {
            return GetListValueInBool(CristalKey, cristalNumber);
        }
        public static bool isMagicMaterialCollacted(int materialNumber)
        {
            return GetListValueInBool(MagicMaterialKey, materialNumber);
        }
        public static bool isCommonItemCollacted(int commonItemNumber)
        {
            return GetListValueInBool(CommonItemKey, commonItemNumber);
        }
    
    }

}