using System.Collections.Generic;
using UnityEngine;

public class weaponsManager : MonoBehaviour
{
    List<IWeapon> weapons = new List<IWeapon>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

}
