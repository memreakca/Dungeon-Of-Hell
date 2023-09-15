using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject fist;
    [SerializeField] GameObject trident;
    [SerializeField] GameObject fireball;
    [SerializeField] GameObject frostbomb;
    [SerializeField] GameObject sword;


    private GameObject currentWeapon;
    int i = 0;

    public void BuildTrident()
    {
        
        if (Character.main.weaponslot[i].childCount < 1)
        {
            currentWeapon = Instantiate(trident, Character.main.weaponslot[i].position, Quaternion.identity);
            currentWeapon.transform.parent = Character.main.weaponslot[i];
        }
        else i++;
    }
    public void BuildFist()
    {

        if (Character.main.weaponslot[i].childCount < 1)
        {
            currentWeapon = Instantiate(fist, Character.main.weaponslot[i].position, Quaternion.identity);
            currentWeapon.transform.parent = Character.main.weaponslot[i];
        }
        else i++;
    }

    public void BuildFireball()
    {

        if (Character.main.weaponslot[i].childCount < 1)
        {
            currentWeapon = Instantiate(fireball, Character.main.weaponslot[i].position, Quaternion.identity);
            currentWeapon.transform.parent = Character.main.weaponslot[i];
        }
        else i++;
    }

    public void BuildFrostBomb()
    {

        if (Character.main.weaponslot[i].childCount < 1)
        {
            currentWeapon = Instantiate(frostbomb, Character.main.weaponslot[i].position, Quaternion.identity);
            currentWeapon.transform.parent = Character.main.weaponslot[i];
        }
        else i++;
    }
    public void BuildSword()
    {

        if (Character.main.weaponslot[i].childCount < 1)
        {
            currentWeapon = Instantiate(sword, Character.main.weaponslot[i].position, Quaternion.identity);
            currentWeapon.transform.parent = Character.main.weaponslot[i];
        }
        else i++;
    }
}
