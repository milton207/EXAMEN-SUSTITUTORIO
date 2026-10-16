using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Arrastra aquí al Player
    public Vector3 offset = new Vector3(0f, 6f, -8f);
    public float rotationSmooth = 4f;  // más bajo = más suave/lento
    public float lookHeight = 1.2f;

    private float currentYaw;

    void Start()
    {
        if (target != null)
            currentYaw = target.eulerAngles.y;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Suavizar solo el ángulo (maneja bien el paso de 359° a 0°)
        currentYaw = Mathf.LerpAngle(currentYaw, target.eulerAngles.y, rotationSmooth * Time.deltaTime);

        // La cámara orbita en arco alrededor del jugador
        Quaternion rotation = Quaternion.Euler(0f, currentYaw, 0f);
        transform.position = target.position + rotation * offset;

        // Mirar al jugador directamente
        transform.LookAt(target.position + Vector3.up * lookHeight);
    }
}