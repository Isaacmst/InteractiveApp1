using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        if (target != null) // Verifica si el target existe
        {
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
        else
        {
            Debug.LogWarning("CameraFollow: Target is null!");
        }
    }
}