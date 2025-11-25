using System.Numerics;
using UnityEngine;

public class pistoletAuto : MonoBehaviour, IWeapon
{
    public float vitesseAttaque = 1f;
    protected float tempsAvantProchaineAttaque = 0f;
    public float vitesseProjectile = 10f;
    public int degats = 10;
    protected GameObject projectilePrefab;
    public GameObject departTire;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        projectilePrefab = Resources.Load<GameObject>("Prefab/Armes/projectile");
    }

    // Update is called once per frame
    public void updateWeapon()
    {
        // tant que le temps avant la prochaine attaque est supérieur à 0 on le décrémente
        if (tempsAvantProchaineAttaque > 0)
        {
            tempsAvantProchaineAttaque -= Time.deltaTime;
        }
    }
    
    public void essaieAttaque(GameObject ennemie)
    {
        if(tempsAvantProchaineAttaque<=0)
        {
            attaque(ennemie);
            tempsAvantProchaineAttaque = 1f / vitesseAttaque;
        }
    }

    public void attaque(GameObject ennemie)
    {
        // on calcule la direction du projectile
        UnityEngine.Vector3 emplacementEnnemie = ennemie.GetComponent<Collider2D>().bounds.center;
        UnityEngine.Vector3 direction = (emplacementEnnemie - departTire.transform.position).normalized;

        // on crée le projectile
        GameObject projectile = Instantiate(projectilePrefab, departTire.transform.position, UnityEngine.Quaternion.identity);

        // on assigne les dégâts au projectile
        degatProjectil degatProj = projectile.GetComponent<degatProjectil>();
        if (degatProj != null)
        {
            degatProj.degats = degats;
        }

        // on annule la gravité et on applique une vélocité au projectile
        Rigidbody2D rbP = projectile.GetComponent<Rigidbody2D>();
        if (rbP != null)
        {
            rbP.gravityScale = 0;
            rbP.linearVelocity = direction.normalized * vitesseProjectile;
        }
    }
}
