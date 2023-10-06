using UnityEngine;

public class BGCode : MonoBehaviour
{

    [SerializeField] private Vector2 lateBG;
    private Transform cameraTransform;
    private Vector3 lateCamerePos;
    

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lateCamerePos = cameraTransform.position;
        
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lateCamerePos;
        transform.position += deltaMovement;
        lateCamerePos = cameraTransform.position;

        
    }
}
