using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csv : MonoBehaviour
{
    void Start()
    {
        // �ؽ�Ʈ ����. �ڵ����� Resources ������ �������� �Ѵ�.
        TextAsset tempTextAsset = Resources.Load<TextAsset>("temp.txt");
        Debug.Log(tempTextAsset.text);
    }

    void Update()
    {
        
    }
}
