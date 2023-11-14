using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour {

    int HP;
    GameObject destination;
    NavMeshAgent agent;
    Rigidbody rb;
    Animator Anim;

    // Start is called before the first frame update
    void Start(){
        HP = 150;
        destination = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        Anim = GetComponent<Animator>();
    }

    //Called when Zombie is damaged
    public void Damaged(int Dmg){        
        if (HP >= 0){
            Anim.SetTrigger("Damaged");
            HP -= Dmg;
            if (HP <= 0){                
                rb.isKinematic = false;
                rb.AddForce(new Vector3(1,1,0),ForceMode.Impulse);
                agent.enabled = false;
                Anim.SetTrigger("Dead");
                StartCoroutine("ClearDead");
            }
        }
    }

    IEnumerator ClearDead()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }

    //If further than 3m and alive navigate to player
    void Navigate(){
        if (agent.enabled == true){
            float distance = Vector3.Distance(transform.position, destination.transform.position);
            if (distance > 3){
                Anim.SetTrigger("Moving");                
                agent.SetDestination(destination.transform.position);                
            }
            else{
                Anim.SetTrigger("Idle");
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
