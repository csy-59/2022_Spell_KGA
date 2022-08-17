using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csv : MonoBehaviour
{
    void Start()
    {
        // 텍스트 파일. 자동으로 Resources 파일을 기준으로 한다.
        TextAsset tempTextAsset = Resources.Load<TextAsset>("temp.txt");
        Debug.Log(tempTextAsset.text);
    }

    void Update()
    {
        
    }
}
