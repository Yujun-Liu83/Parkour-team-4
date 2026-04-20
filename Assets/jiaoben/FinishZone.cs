using UnityEngine;

public class FinishZone : MonoBehaviour
{
    public GameTimer gameTimer;

    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
        {
            player = other.GetComponentInParent<PlayerMovement>();
        }

       
           FindAnyObjectByType<TitleController>().WinGame();

    }
}