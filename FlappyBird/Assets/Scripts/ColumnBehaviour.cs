using UnityEngine;

public class ColumnBehaviour : MonoBehaviour
{
    public AudioSource PointAudio;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            GameController.Instance.Score += 1;
            PointAudio.Play();
        }
    }
}
