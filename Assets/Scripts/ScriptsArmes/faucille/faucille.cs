using UnityEngine;

public class faucille : MonoBehaviour, IWeapon
{
    public int nombreProjectiles = 3;
    public float vitesseRotation = 1f;
    public float distance = 3f;
    public int degats = 10;
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
        for(int i=0; i<nombreProjectiles; i++)
        {
            // maintenir la distance entre la faucille et le joueur
            Vector3 direction = (transform.position - departTire.transform.position).normalized;
            transform.position = departTire.transform.position + direction * distance;

            GameObject projectile = Instantiate(projectilePrefab, departTire.transform.position + direction * distance, Quaternion.identity);

            // on assigne les dégâts au projectile
            projectileFaucilleManager paramProj = projectile.GetComponent<projectileFaucilleManager>();
            if (paramProj != null)
            {
                paramProj.degats = degats;
            }
        }
        // faire tourner la faucille autour du joueur
        transform.RotateAround(departTire.transform.position, Vector3.forward, vitesseRotation * Time.deltaTime);
        
    }
    
    public void essaieAttaque(GameObject ennemie)
    {
        
    }

}
