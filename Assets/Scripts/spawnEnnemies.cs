using System.Collections;
using UnityEngine;

public class spawnEnnemies : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject player;
    public float spawnRadius = 5f;
    public float spawnInterval = 3f;
    public int nbEnemies = 3;
    public float increaseInterval = 5f;
    public int increaseAmount = 3;


    public void Start()
    {
        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(increaseEnemiesRoutine());
    }


    public void SpawnEnemy()
    {
        // angle aléatoire en radians
        float angle = Random.Range(0f, Mathf.PI * 2f);

        // calcul de la position sur le cercle
        Vector2 spawnPosition = new Vector2(
            player.transform.position.x + Mathf.Cos(angle) * spawnRadius,
            player.transform.position.y + Mathf.Sin(angle) * spawnRadius
        );

        // instanciation de l'ennemi
        GameObject ennemie = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        // on assigne le joueur à l'ennemi ici parce qu'on peut pas le faire dans l'inspector
        ennemie.GetComponent<bougeEnnemies>().joueur = player;
    }

    IEnumerator SpawnEnemiesRoutine()
    {
        // on attends deux secondes avant la première vague de spawn
        yield return new WaitForSeconds(2);

        while (true)
        {
            for (int i = 0; i < nbEnemies; i++) SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator increaseEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(increaseInterval);
            nbEnemies += increaseAmount;
        }
    }
    
}