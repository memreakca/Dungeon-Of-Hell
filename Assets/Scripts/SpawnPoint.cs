using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static SpawnPoint main;
    private void Awake()
    {
        main = this;
    }

    public void spawn()
    {
        gameObject.SetActive(true);
        Invoke("setactivefalse", 0.2f);
    }

    public void setactivefalse()
    {
        gameObject.SetActive(false);
    }
}
