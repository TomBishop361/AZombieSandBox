using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour {

    int HP;
    GameObject Player;
    public GameObject[] Windows;
    GameObject destination;
    NavMeshAgent agent;    
    Animator Anim;
    NavMeshPath path;
    NavMeshPath DistPath;
    bool Attacking;

    // Start is called before the first frame update
    void Start(){
        Attacking = false;
        HP = 150;        
        Windows = GameObject.FindGameObjectsWithTag("Window");
        Player = GameObject.FindGameObjectWithTag("Player");
        path = new NavMeshPath();
        destination = Player;
        agent = GetComponent<NavMeshAgent>();        
        Anim = GetComponent<Animator>();
    }
   

    //Called when Zombie is damaged
    public void Damaged(int Dmg){        
        if (HP >= 0){
            Anim.SetTrigger("Damaged");
            HP -= Dmg;
            if (HP <= 0){  
                agent.enabled = false;
                transform.GetComponent<CapsuleCollider>().enabled = false;
                Anim.SetTrigger("Dead");
                Player.GetComponent<ShootWeapon>().Points += 100;
                StartCoroutine("ClearDead");
            }
        }
    }

    IEnumerator ClearDead() {
        yield return new WaitForSeconds(2.5f);
        Destroy(this.gameObject);
    }

    //If further than 3m and alive navigate to player
    void Navigate(){
        Debug.Log("WORKING");
        agent.CalculatePath(destination.transform.position, path);
        switch (path.status){   
            case NavMeshPathStatus.PathComplete:
                Debug.Log("PathComplete");
                if (agent.enabled == true){
                    Debug.Log("PathComplete");
                    if (destination != Player){
                        Debug.Log("Destination Player");
                        destination = Player;                        
                    }
                    float distance = Vector3.Distance(transform.position, destination.transform.position);
                    if (distance > 2)
                    {
                        Anim.SetTrigger("Moving");
                        agent.SetDestination(destination.transform.position);
                        
                    }
                    else if( distance < 2 && destination == Player) {                        
                        Anim.SetTrigger("Idle");
                        agent.SetDestination(transform.position);
                        if (!Attacking){
                            StartCoroutine("Attack");
                        }
                        
                    }
                    else
                    {
                        Anim.SetTrigger("Idle");
                        agent.SetDestination(transform.position);
                    }
                }
                Debug.Log("Break");
                break;
            case NavMeshPathStatus.PathPartial:
                Debug.Log("Partial");                
                if (agent.enabled == true){
                    Debug.Log("Destination Window");
                    destination = Windows[0];                    
                    float distance = Vector3.Distance(transform.position, destination.transform.position);
                    if (distance > 2)
                    {
                        Anim.SetTrigger("Moving");
                        agent.SetDestination(destination.transform.position);
                    }
                    else
                    {
                        Anim.SetTrigger("Idle");
                        agent.SetDestination(transform.position);
                    }
                }
                Debug.Log("Break");
                break;
            case NavMeshPathStatus.PathInvalid:
                Debug.Log("Invalid");
                destination = Player;
                break;
        }        
    }
    
    IEnumerator Attack() {
        Attacking = true;
        Anim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        Player.GetComponent<PlayerHealthScript>().Attacked();
        while (Attacking){
            agent.enabled = false;            
            yield return new WaitForSeconds(2);
            Attacking = false;
        }
        agent.enabled = true;
       }

    public void AttackBarrier()
    {
        Anim.SetTrigger("Attack");
    }


    private void Update(){
        Windows = Windows.OrderBy((d) => (d.transform.position - transform.position).sqrMagnitude).ToArray();
        if(agent.enabled == true)
        {
            Navigate();
        }
        
    }

    // Update is called once per frame
    void FixedUpdate(){       
    }
     
}
