using System.Collections.Generic;
using UnityEngine;

public class weaponsManager : MonoBehaviour
{
    List<IWeapon> weapons = new List<IWeapon>();
    private List<GameObject> EnnemiesDansZone = new();

    void Start()
    {
        // Récupère seulement les armes actives dans les enfants, donc les armes désactivées ne seront pas incluses
        weapons.AddRange(GetComponentsInChildren<IWeapon>());
    }

    // Update is called once per frame
    void Update()
    {
        foreach(IWeapon weapon in weapons)
        {
            weapon.updateWeapon();
        }
    }

    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "ennemie")
        {
            foreach(IWeapon weapon in weapons)
            {
                weapon.essaieAttaque(trigger.gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ennemie") EnnemiesDansZone.Add(collision.gameObject);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ennemie") EnnemiesDansZone.Remove(collision.gameObject);
    }

    public GameObject ChoisirEnnemi()
    {
        if (EnnemiesDansZone == null || EnnemiesDansZone.Count == 0)
            return null;

        // Ennemi aléatoire !
        int index = Random.Range(0, EnnemiesDansZone.Count);
        return EnnemiesDansZone[index];
    }

}
