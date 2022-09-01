using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasToFollowEye : MonoBehaviour
{
    [SerializeField] private Transform eye;
    private Vector3 canvasOffset;

    private void Awake()
    {
        canvasOffset = transform.position - eye.position;
    }

    void Update()
    {
        transform.position = eye.position + canvasOffset;
        transform.rotation = eye.rotation;
    }
}
