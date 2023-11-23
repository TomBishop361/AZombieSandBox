using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    bool ZomAttack = false;
   

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other){
        Debug.Log("Entered");
        if (this.transform.tag == ("Player")){
            transform.GetComponent<ShootWeapon>().Repairable = true;
        }    
        

    }

    private void OnTriggerStay(Collider other){
        if(this.transform.tag == ("Zombie")){                        
            ZomAttack = other.gameObject.GetComponent<ZomBarricadeScript>().damageBarricade();
            //ZomAttack = false; 
            
        }
        Debug.Log("STAY");

    }

    private void Update(){
        if(transform.tag == ("Zombie")){
            if (ZomAttack){
                transform.GetComponent<ZombieScript>().Attack();
            }
        }
    }

}
