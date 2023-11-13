using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour {

    int HP;
    GameObject destination;
    NavMeshAgent agent;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start(){
        HP = 150;
        destination = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }


    //Called when Zombie is damaged
    public void Damaged(int Dmg){        
        if (HP >= 0){            
            HP -= Dmg;
            if (HP <= 0){                
                rb.isKinematic = false;
                rb.AddForce(new Vector3(1,1,0),ForceMode.Impulse);
                agent.enabled = false;
                new WaitForSeconds(3);
                Destroy(gameObject);
            }
        }
    }

    //If further than 3m and alive navigate to player
    void Navigate(){
        if (agent.enabled == true){
            float distance = Vector3.Distance(transform.position, destination.transform.position);
            agent.SetDestination(destination.transform.position);
            if (distance < 3)
            {
               agent.SetDestination(transform.position);
            }
        }
    }

    private void Update(){

        Navigate();
    }

    // Update is called once per frame
    void FixedUpdate(){       
    }
     
}
