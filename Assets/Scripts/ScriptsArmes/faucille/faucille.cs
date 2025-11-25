using UnityEngine;

public class faucille : MonoBehaviour, IWeapon
{
    public int nombreProjectiles = 3;
    public float vitesseAttaque = 1f;
    public float rayonAttaque = 3f;
    public float vitesseProjectile = 10f;
    public int degats = 10;
    protected GameObject projectilePrefab;
    public GameObject departTire;


    void Start()
    {
        projectilePrefab = Resources.Load<GameObject>("Prefab/Armes/projFaucille");
    }

    public void updateWeapon()
    {
        
    }
    
    public void essaieAttaque(GameObject ennemie)
    {
        attaque(ennemie);
    }

    public void attaque(GameObject ennemie)
    {
        // on calcul les angles entre chaque projectile
        float angleEntreProjectiles = 360f / nombreProjectiles;

        // on calcule l'emplacement du projectile repère
        Vector3 targetPoint = transform.position + transform.up * rayonAttaque;
        Vector3 direction = (targetPoint - departTire.transform.position).normalized;

        // on calcule l'orientation du projectile repère
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        for(int i=0; i<nombreProjectiles; i++)
        {
            float angleProjectile = angle + i * angleEntreProjectiles;
            // convertir l'angle en direction (vecteur)
            float angleRadians = angleProjectile * Mathf.Deg2Rad;
            Vector3 directionProjectile = new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians), 0);

            // on crée le projectile
            GameObject projectile = Instantiate(projectilePrefab, departTire.transform.position, Quaternion.identity);

            // on oriente le projectile (j'ai mis +270 à l'angle pour que le sprite soit dans le bon sens, sinon il était perpendiculaire à la direction et j'ai la flemme de savoir pourquoi)
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleProjectile+270));

            // on assigne les dégâts au projectile
            projectileFaucilleManager degatProj = projectile.GetComponent<projectileFaucilleManager>();
            if (degatProj != null)
            {
                degatProj.degats = degats;
            }

            // on annule la gravité et on applique une vélocité au projectile
            Rigidbody2D rbP = projectile.GetComponent<Rigidbody2D>();
            if (rbP != null)
            {
                rbP.gravityScale = 0;
            }
        }

    }

}
