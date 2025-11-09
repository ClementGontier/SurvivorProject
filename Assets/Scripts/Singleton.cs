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
    [SerializeField] private Animator animdeath;
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
        
        pvText.text = "Vies : " + playerHealth.ToString();
        if (playerHealth > 0)
        {
            playerHealth -= damage;
        }else{
            playerHealth = 0;

            animdeath.SetBool("isNotAlive", true);
            Debug.Log("Joueur mort");
        }
    }
}
