using UnityEngine;

public class bougeEnnemies : MonoBehaviour
{

    public GameObject joueur;
    public float speed = 2f;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // direction vers le joueur
        Vector2 direction = (joueur.transform.position - transform.position).normalized;

        // déplacement
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
        animator.Play("Move");
    }
}