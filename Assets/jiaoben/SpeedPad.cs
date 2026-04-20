
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    public float boostDuration = 2f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SpeedPad touched: " + other.name);

        PlayerMovement player = other.GetComponent<PlayerMovement>();

        if (player == null)
        {
            player = other.GetComponentInParent<PlayerMovement>();
        }

        if (player != null)
        {
            Debug.Log("Player got speed boost");
            player.ApplySpeedBoost(boostDuration);
        }
        else
        {
            Debug.Log("No PlayerMovement found on: " + other.name);
        }
    }
}