using UnityEngine;

public class degatProjectil : MonoBehaviour
{
    public int degats = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ennemie")
        {
            vieEnnemies vieEnnemi = collision.gameObject.GetComponent<vieEnnemies>();
            if (vieEnnemi != null)
            {
                vieEnnemi.prendsDegats(degats);
                //Debug.Log("L'ennemie prend " + degats + " degats");
            }
            Destroy(gameObject);
        }
    }


}
