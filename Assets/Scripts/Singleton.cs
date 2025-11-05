using TMPro;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }
    public TMP_Text pvText;
    public int playerMaxHealth = 10;
    public int playerHealth = 10;
    public int playerXP = 0;
    public int playerLevel = 1;

    void Awake()
    {
        // Si une instance existe déjà et que ce n’est pas celle-ci, on détruit celle en trop
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Sinon, on garde celle-ci
        Instance = this;
        DontDestroyOnLoad(gameObject); // garde le singleton entre les scènes
    }

    public void TakeDamage(int damage)
    {
        playerHealth -= damage;
        pvText.text = "Vie : " + playerHealth.ToString();
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            Debug.Log("Joueur mort");
        }
    }
}
