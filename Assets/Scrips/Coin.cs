using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSound;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si quien toca la moneda es el Jugador
        if (other.CompareTag("Player"))
        {
            // Reproducir sonido si está asignado
            if (coinSound != null)
            {
                AudioSource.PlayClipAtPoint(coinSound, transform.position);
            }

            // Sumar puntos en el GameManager (lo crearemos en el siguiente paso)
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddCoin();
            }

            // Destruir la moneda de la escena
            Destroy(gameObject);
        }
    }
}