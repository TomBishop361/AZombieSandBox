using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZomBarricadeScript : MonoBehaviour
{
    [SerializeField]GameObject Link;
    [SerializeField]GameObject[] Barricades;
    int barricadesCount;

    void Start(){
        barricadesCount = 4;        
    }

    //Destroy a barricade and decreases the BarricadeCount
    public void damageBarricade(){
        if (barricadesCount > 0){
            Barricades[barricadesCount].gameObject.SetActive(false);
            barricadesCount--;
        }
    }
    
    public void RepairBarricade(){
        if (barricadesCount < 3){
            Barricades[barricadesCount].gameObject.SetActive(true);
            barricadesCount++;
        }
    }

}
