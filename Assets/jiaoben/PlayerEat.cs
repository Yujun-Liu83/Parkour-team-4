using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class PlayerEat : MonoBehaviour
{
    public int currentCount = 0;
    public int targetCount = 5;

    public Text countText;
    public Text resultText;

    void Start()
    {
        countText.text = "0";
        //resultText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("碰到了: " + other.name);

        Food food = other.GetComponent<Food>();

        if (food == null)
        {
            food = other.GetComponentInParent<Food>();
        }

        if (food != null)
        {
            Debug.Log("吃到了食物: " + food.name);

            Destroy(food.gameObject);
            currentCount++;
            countText.text = currentCount.ToString();

            if (currentCount >= targetCount)
            {
               FindAnyObjectByType<TitleController>().WinGame();
            }
        }
    }
}