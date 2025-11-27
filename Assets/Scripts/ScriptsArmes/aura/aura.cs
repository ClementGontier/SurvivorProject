using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class aura : MonoBehaviour, IWeapon
{
    public float vitesseAttaque = 1f;
    public int degats = 10;
    public float taille = 5;
    private float tempsAvantProchaineAttaque = 0f;
    private List<GameObject> EnnemiesDansZone = new();

    // Update is called once per frame
    public void Update()
    {
        // tant que le temps avant la prochaine attaque est supérieur à 0 on le décrémente
        if (tempsAvantProchaineAttaque > 0)
        {
            tempsAvantProchaineAttaque -= Time.deltaTime;
        }
        
        transform.localScale = new Vector3(taille, taille, 0);

        if (tempsAvantProchaineAttaque <= 0)
        {
            attaque();
            tempsAvantProchaineAttaque = 1f / vitesseAttaque;
        }
    }

    public void updateWeapon()
    {
        
    }
    
    public void essaieAttaque(GameObject ennemie)
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ennemie") EnnemiesDansZone.Add(collision.gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ennemie") EnnemiesDansZone.Remove(collision.gameObject);
    }

    public void attaque()
    {
        foreach(GameObject ennemie in EnnemiesDansZone)
        {
            vieEnnemies vieEnnemi = ennemie.GetComponent<vieEnnemies>();
            if (vieEnnemi != null)
            {
                vieEnnemi.prendsDegats(degats);
                // Debug.Log("L'ennemie prend " + degats + " degats");
            }
        }
    }
}
