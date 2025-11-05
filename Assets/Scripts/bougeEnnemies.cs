
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
        Vector3 move = (Vector3)(direction * speed * Time.deltaTime);

        float moveX = move.x, moveY = move.y;


        if (moveX != 0)
        {
            if (moveX > 0)
            {
                joueur.GetComponent<SpriteRenderer>().flipX = false;
                animator.SetBool("MoveTrigger", true);
            }
            else if (moveX < 0)
            {
                joueur.GetComponent<SpriteRenderer>().flipX = true;
                animator.SetBool("MoveTrigger", true);
            }
        }
        else if (moveY != 0)
        {
            if (moveY > 0)
            {
                animator.SetBool("MoveTrigger", true);
            }
            else if (moveY < 0)
            {
                animator.SetBool("MoveTrigger", true);
            }
        }
        else
        {
            animator.SetBool("MoveTrigger", false);
        }
        transform.position += move;
    }
}