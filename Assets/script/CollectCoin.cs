using UnityEngine;
using UnityEngine.UI;

public class CollectCoin : MonoBehaviour
{
    public int collectedCoins = 0;
    public AudioClip coinSound;
    private AudioSource audioSource;
    public Text coinText; // Reference to the UI Text component

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        UpdateCoinText();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            collectedCoins++;
            UpdateCoinText();
            audioSource.PlayOneShot(coinSound);
            Destroy(other.gameObject);
        }
    }

    void UpdateCoinText()
    {
        coinText.text = "Coins: " + collectedCoins.ToString();
        Debug.Log(collectedCoins);
    }
}
