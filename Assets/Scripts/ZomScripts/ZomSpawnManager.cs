using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEngine.Rendering.DebugUI;

public class ZomSpawnManager : MonoBehaviour
{
    bool Restarting;
    public GameObject Player;
    public GameObject[] Spawners;
    public int TrgtZomCount =15;
    public int ZombieCount=0;
    public GameObject[] AliveZombies;
    float SpawnMultiplier = 1.2f;

    // Start is called before the first frame update
    void Start(){        
        Spawners = GameObject.FindGameObjectsWithTag("ZombieSpawnPoint");
        StartCoroutine("SummonZombies");
    }


    //Called when zombies should be spawned
    IEnumerator SummonZombies(){
        //While the number of zombies spawned is less than the Amount that should be spawned    
        while (ZombieCount < TrgtZomCount){
            //X = how many of the nearest Spawn points the spawnManager will call 'SpawnZombie' in)
            int x = 4;            
            for (int i = 0; i < x; i++)            {
                Spawners[i].GetComponent<ZombieSpawner>().SpawnZombie();
                ZombieCount++;
                
            }
            yield return new WaitForSeconds(2.5f);
        }      
        
    }

    IEnumerator RoundRestart(){
        //Resets Variable values, Waits 10 seconds then starts next round buy calling SummonZombies
        float tempfloat = TrgtZomCount;
        TrgtZomCount = (int)(tempfloat * SpawnMultiplier);
        ZombieCount = 0;
        SpawnMultiplier += 0.1f;
        yield return new WaitForSeconds(10);
        Restarting = false;
        StartCoroutine("SummonZombies");
    }

        // Update is called once per frame
    void Update(){
        //This Orders the array from closest to player to furthest
        Spawners = Spawners.OrderBy((d) => (d.transform.position - Player.transform.position).sqrMagnitude).ToArray();
        AliveZombies = GameObject.FindGameObjectsWithTag("Zombie");
        if (AliveZombies.Length <= 0 && ZombieCount <= TrgtZomCount && !Restarting)
        {
            Restarting = true;
            Debug.Log("RESTART ROUND");
            StartCoroutine("RoundRestart");
        }




    }
}
