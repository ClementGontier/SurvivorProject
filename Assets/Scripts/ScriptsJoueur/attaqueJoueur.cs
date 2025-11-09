using System.Numerics;
using UnityEngine;

public class attaqueJoueur : MonoBehaviour
{
    public float vitesseAttaque = 1f;
    protected float tempsAvantProchaineAttaque = 0f;
    public float vitesseProjectile = 10f;
    public int degats = 10;
    protected GameObject projectilePrefab;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        projectilePrefab = Resources.Load<GameObject>("Prefab/Armes/projectile");
    }

    // Update is called once per frame
    void Update()
    {
        if (tempsAvantProchaineAttaque > 0)
        {
            tempsAvantProchaineAttaque -= Time.deltaTime;
        }
    }

    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "ennemie")
        {
            essaieAttaque(trigger.gameObject);
        }
    }
    
    private void essaieAttaque(GameObject ennemie)
    {
        if(tempsAvantProchaineAttaque<=0)
        {
            attaque(ennemie);
            tempsAvantProchaineAttaque = 1f / vitesseAttaque;
        }
    }

    private void attaque(GameObject ennemie)
    {
        UnityEngine.Vector3 emplacementEnnemie = ennemie.transform.position;
        UnityEngine.Vector3 direction = emplacementEnnemie - transform.position;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, UnityEngine.Quaternion.identity);
        Rigidbody2D rbP = projectile.GetComponent<Rigidbody2D>();
        if (rbP != null)
        {
            rbP.linearVelocity = direction.normalized * vitesseProjectile;
        }
    }
}
