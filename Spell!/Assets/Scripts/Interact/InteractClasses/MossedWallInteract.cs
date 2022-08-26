using InteractAsset;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossedWallInteract : InteractiveObject
{
    [Header("Moss Interact")]
    [SerializeField] private int mossCount;
    [SerializeField] private GameObject moss;
    
    [SerializeField] private Vector3 minMossPosition;
    [SerializeField] private Vector3 maxMossPosition;

    [SerializeField] private float minScale = 0.06f;
    [SerializeField] private float maxScale = 0.11f;

    [SerializeField] private float mossPickForce = 2f;
    
    private Stack<GameObject> mossStack = new Stack<GameObject>();

    protected override void Awake()
    {
        base.Awake();

        for(int i = 0; i < mossCount; ++i)
        {
            float x = Random.Range(minMossPosition.x, maxMossPosition.x);
            float y = Random.Range(minMossPosition.y, maxMossPosition.y);
            Vector3 newPosition = new Vector3(x, y, maxMossPosition.z);

            float scale = Random.Range(minScale, maxScale);

            GameObject newMoss = Instantiate(moss, transform);
            newMoss.name = "Moss";
            newMoss.transform.Translate(newPosition);
            newMoss.transform.Rotate(90f, 0f, 0f);
            newMoss.transform.localScale = new Vector3(scale, scale, scale);

            Debug.Log("newMoss");

            mossStack.Push(newMoss);
        }
    }

    public override bool Interact(ItemList item, EffectList effect)
    {
        if(mossStack.Count <= 0)
        {
            return false;
        }

        UIManager.Instance.SetInfoTextBar("It's slimy...");

        GameObject pickedMoss = mossStack.Pop();
        pickedMoss.transform.parent = null;
        Rigidbody mossRigidbody = pickedMoss.GetComponent<Rigidbody>();
        pickedMoss.GetComponent<BoxCollider>().enabled = true;
        mossRigidbody.useGravity = true;
        mossRigidbody.AddForce(Vector3.forward * mossPickForce, ForceMode.Impulse);

        if(mossStack.Count <= 0)
        {
            gameObject.name = "Cleaned Wall";
        }
        return true;
    }

}
