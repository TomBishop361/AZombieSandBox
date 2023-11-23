using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomBarricadeScript : MonoBehaviour
{
    [SerializeField]GameObject Link;
    [SerializeField]GameObject[] Barricades;
    int barricadesCount;
    bool Attacked = false;
    bool Damaging = false;

    void Start(){
        barricadesCount = 4;        

    }

    //Destroy a barricade and decreases the BarricadeCount
    public bool damageBarricade(){
        StartCoroutine(DOT());       
        return Attacked;
       
    }

   private IEnumerator DOT(){       
        
        if (!Damaging){
            Attacked = false;
            Damaging = true;
            yield return new WaitForSeconds(3.5f);
            if (barricadesCount > 0)
            {
                Barricades[barricadesCount - 1].gameObject.SetActive(false);
                barricadesCount--;
                Attacked = true;
                yield return Attacked;
            }
            else { yield return null; }
            Damaging=false;
        }
        
        
    }
    
    public void RepairBarricade(){
        if (barricadesCount < 3){
            Barricades[barricadesCount].gameObject.SetActive(true);
            barricadesCount++;
        }
    }

    private void Update(){
        if(barricadesCount == 0){
            Link.SetActive(true);
        }
        else
        {
            Link.SetActive(false);
        }
    }
}
