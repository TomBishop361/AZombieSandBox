using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] GameObject Zombie;
    
    public void SpawnZombie(){
        Instantiate(Zombie, transform.position, transform.rotation);
    }
}
