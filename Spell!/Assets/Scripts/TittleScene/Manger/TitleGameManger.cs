using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InteractAsset;
using UtilityAsset;

public class TitleGameManger : SingletonBehaviour<TitleGameManger>
{
    private int collectedScrollCount = 0;
    private int collectedCristalCount = 0;
    private int collectedCollectionCount = 0;

    private void Awake()
    {
        CollectedCount(PlayerPrefsKey.ScrollKey, (int)ScrollList.ScrollMax, ref collectedScrollCount);
        CollectedCount(PlayerPrefsKey.CristalKey, (int)CristalList.CristalMax, ref collectedCristalCount);
        CollectedCount(PlayerPrefsKey.CommonItemKey, (int)CommonItemList.CommonItemMax, ref collectedCollectionCount);
    }
    private void CollectedCount(string key, int maxCount, ref int collectedCount)
    {
        for(int i = 0; i< maxCount; ++i)
        {
            if(IsCollactiveObejctCollected(key, i))
            {
                ++collectedCount;
            }
        }
    }

    public bool IsCollactiveObejctCollected(string key, int number)
    {
        return PlayerPrefsKey.GetListValueInBool(key, number);
    }
}
