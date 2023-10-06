using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;


    private void Update()
    {
        
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
