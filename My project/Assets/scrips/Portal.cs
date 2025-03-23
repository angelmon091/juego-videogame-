using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform linkedPortal; // El portal al que este portal está conectado
    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            canTeleport = false;
            Teleport(other.transform);
            Invoke("ResetTeleport", 10f); // Espera 1 segundo antes de permitir otro teletransporte
        }
    }

    void Teleport(Transform objectToTeleport)
    {
        // Calcula la posición relativa del objeto respecto al portal
        Vector3 relativePosition = transform.InverseTransformPoint(objectToTeleport.position);
        relativePosition = new Vector3(-relativePosition.x, -relativePosition.y, relativePosition.z);

        // Teletransporta el objeto al portal vinculado
        objectToTeleport.position = linkedPortal.TransformPoint(relativePosition);

        // Mueve ligeramente al jugador para evitar que active el trigger del segundo portal
        objectToTeleport.Translate(Vector2.up * 1f); // Mueve al jugador 0.1 unidades hacia arriba
    }

    void ResetTeleport()
    {
        canTeleport = true;
    }
}

