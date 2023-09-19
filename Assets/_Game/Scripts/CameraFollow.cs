using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    private void Start()
    {
        target = FindObjectOfType<Player>().transform;
    }

    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
}
