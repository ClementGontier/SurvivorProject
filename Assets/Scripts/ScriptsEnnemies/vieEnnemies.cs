using UnityEngine;

public class vieEnnemies : MonoBehaviour
{
    public int vieMax;
    protected int vieActuelle;
    public int degats;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vieActuelle = vieMax;
    }

    public void prendsDegats(int montant)
    {
        vieActuelle -= montant;
        if (vieActuelle <= 0)
        {
            meurt();
        }
    }
    
    void meurt()
    {
        Destroy(gameObject);
    }
}
