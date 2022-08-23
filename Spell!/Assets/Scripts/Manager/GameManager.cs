using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    //private int lightBit = 0;
    //[SerializeField] private int[] secretLightNumber = { 3, 9 };
    //[SerializeField] private bool isSecretOpen = false;
    //private int secretLightMask;

    //private void Awake()
    //{
    //    foreach(int num in secretLightNumber)
    //    {
    //        secretLightMask = secretLightMask | (1 << num);
    //    }
    //}

    //void Update()
    //{
        
    //}

    //public void LightOn(int lightNumber)
    //{
    //    lightBit = lightBit | (1 << lightNumber);
    //    LightCheck();
    //}

    //public void LightOff(int lightNumber)
    //{
    //    lightBit = lightBit & ~(1 << lightNumber);
    //    LightCheck();
    //}

    //private void LightCheck()
    //{
    //    if(lightBit == secretLightMask)
    //    {
    //        isSecretOpen = true;
    //    }
    //}
}
