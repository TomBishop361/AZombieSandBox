using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameObject Zombie;

    //Spawn a Copy of the Zombie Prefab
    public void SpawnZombie(){
        Instantiate(Zombie, transform.position, transform.rotation);
    }
}
