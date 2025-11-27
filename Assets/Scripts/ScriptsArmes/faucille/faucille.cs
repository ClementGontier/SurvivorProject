using System.Collections;
using UnityEngine;

public class faucille : MonoBehaviour, IWeapon
{
    public float vitesseAttaque = 1f;
    protected float tempsAvantProchaineAttaque = 0f;
    public float vitesseProjectile = 10f;
    public int degats = 10;
    public float distanceAvantDemi = 5f;
    public float taille = 1;
    protected GameObject projectilePrefab;
    public GameObject departTire;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        projectilePrefab = Resources.Load<GameObject>("Prefab/Armes/projFaucille");
    }

    // Update is called once per frame
    public void updateWeapon()
    {
        // tant que le temps avant la prochaine attaque est superieure a 0 on le decremente
        if (tempsAvantProchaineAttaque > 0)
        {
            tempsAvantProchaineAttaque -= Time.deltaTime;
        }
    }
    
    public void essaieAttaque(GameObject ennemie)
    {
        if (tempsAvantProchaineAttaque <= 0)
        {
            attaque(ennemie);
            tempsAvantProchaineAttaque = 1f / vitesseAttaque;
        }
    }

    public void attaque(GameObject ennemie)
    {
        // on calcule la direction du projectile
        Vector3 emplacementEnnemie = ennemie.GetComponent<Collider2D>().bounds.center;
        Vector3 direction = (emplacementEnnemie - departTire.transform.position).normalized;

        // on cree le projectile
        GameObject projectile = Instantiate(projectilePrefab, departTire.transform.position, Quaternion.identity);

        // on assigne les paramètres au projectile
        projectileFaucilleManager paramsProj = projectile.GetComponent<projectileFaucilleManager>();
        if (paramsProj != null)
        {
            paramsProj.degats = degats;
            paramsProj.distanceAvantDemi = distanceAvantDemi;
            paramsProj.directionInitiale = direction;
            paramsProj.vitesseProjectile = vitesseProjectile;
            paramsProj.taille = taille;
        }

        // on annule la gravite et on applique une velocite au projectile
        Rigidbody2D rbP = projectile.GetComponent<Rigidbody2D>();
        if (rbP != null)
        {
            rbP.gravityScale = 0;
            rbP.linearVelocity = direction.normalized * vitesseProjectile;
        }
    }
}
