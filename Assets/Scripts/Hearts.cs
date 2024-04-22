using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public GameObject heartPrefab;
    public Transform heartParent;
    public Timer timer;
    public Fiction fiction;

    private List<GameObject> hearts = new List<GameObject>();

    void Start()
    {
        int maxGuesses = timer.SetDifficulty();
        SetHearts(maxGuesses);
    }

    public void SetHearts(int maxGuesses)
    {
        Vector3 originalPosition = new Vector3(-6.05f, -10.7f, 0.0125129f);
        float spacing = 1.8f; // Adjust this value to increase or decrease spacing

        for (int i = 0; i < maxGuesses; i++)
        {
            GameObject newHeart = Instantiate(heartPrefab, heartParent);
            hearts.Add(newHeart);

            // Calculate offset based on the index and spacing
            float offsetX = (i % 5) * spacing; // 5 hearts per row
            float offsetY = (i / 5) * spacing; // New row every 5 hearts

            Vector3 newPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);
            newHeart.transform.localPosition = newPosition;
        }
    }

    public void RemoveHeart()
    {
        if (hearts.Count > 0)
        {
            // Destroy the last heart in the list
            GameObject lastHeart = hearts[hearts.Count - 1];
            Destroy(lastHeart);

            // Remove the destroyed heart from the list
            hearts.RemoveAt(hearts.Count - 1);
        }
    }
}
