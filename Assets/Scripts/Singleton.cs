using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public float timertime = 60;
    public float timertimeMax = 60;

    [Header("Références Scène")]
    public TMP_Text pvText;
    public TMP_Text timer;
    [SerializeField] private Animator animdeath;

    bool hasLoadedScene = false;

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "MainMenu")
        {
            timertime -= 1 * Time.deltaTime;
            timer.text = timertime.ToString("0");

            if (timertime <= 0 && !hasLoadedScene)
            {
                hasLoadedScene = true;
                timertime = 0;
                ManageScenes.instance.NextLevel();
                timertime = timertimeMax;
            }
        }
    }

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


    /// Se reconnecte aux objets de la nouvelle scène (UI, joueur, etc.)
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Récupération du TMP_Text et de l'Animator de la nouvelle scène
        pvText = DontDestroyUI.instance.GetHealthUI();
        timer = DontDestroyUI.instance.GetTimer();
        animdeath = GameObject.Find("Joueur")?.GetComponent<Animator>();

        // Réinitialiser affichage et état du joueur
        isAlive = true;
        playerHealth = playerMaxHealth;
        timertime = timertimeMax;
        if (scene.name == "MainMenu")
        {
            Destroy(GameObject.Find("Joueur"));
            Debug.Log("scene false");
        }
        if (timer != null)
            timer.text = "Temps : " + timertime.ToString();
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
            DontDestroyUI.instance.healthUI.gameObject.SetActive(false);
            DontDestroyUI.instance.timer.gameObject.SetActive(false);
           

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
