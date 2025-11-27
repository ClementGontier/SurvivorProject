using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Singleton Instance { get; private set; }

    [Header("Stats Joueur")]
    public int playerMaxHealth = 10;
    public int playerHealth = 10;
    public int playerXP = 0;
    public int playerLevel = 1;
    public bool isAlive = true;
    public bool isInvincible = false;

    [Header("Références Scène")]
    public TMP_Text pvText;
    [SerializeField] private Animator animdeath;

    private void Awake()
    {
        // Gestion du singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Se reconnecte aux objets de la nouvelle scène (UI, joueur, etc.)
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Récupération du TMP_Text et de l'Animator de la nouvelle scène
        pvText = GameObject.Find("PVText")?.GetComponent<TMP_Text>();
        animdeath = GameObject.Find("Joueur")?.GetComponent<Animator>();

        // Réinitialiser affichage et état du joueur
        isAlive = true;
        playerHealth = playerMaxHealth;

        if (pvText != null)
            pvText.text = "Vies : " + playerHealth.ToString();

        if (animdeath != null)
            animdeath.SetBool("isNotAlive", false);
    }

    // Ajouter un ICD (invincibilité temporaire après être attaqué)
    public void TakeDamage(int damage)
    {
        if (!isAlive || isInvincible)
            return;

        playerHealth -= damage;
        StartCoroutine(ICD());
        if (playerHealth < 0) playerHealth = 0;

        if (pvText != null)
            pvText.text = "Vies : " + playerHealth.ToString();

        if (playerHealth <= 0 && isAlive)
        {
            isAlive = false;

            if (animdeath != null)
                animdeath.SetBool("isNotAlive", true);

            ManageScenes.instance.gameOver();
        }
    }


    private IEnumerator ICD()
    {
        isInvincible = true;
        Debug.Log("Player is invincible for 1 second.");
        yield return new WaitForSeconds(1f);
        isInvincible = false;
    }

}
