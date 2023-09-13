using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject fist;
    [SerializeField] GameObject trident;


    private GameObject currentWeapon;
    int i = 0;

    public void BuildIt()
    {
        
        if (Character.main.weaponslot[i].childCount < 1)
        {
            currentWeapon = Instantiate(trident, Character.main.weaponslot[i].position, Quaternion.identity);
            currentWeapon.transform.parent = Character.main.weaponslot[i];
        }
        else i++;
    }
}
