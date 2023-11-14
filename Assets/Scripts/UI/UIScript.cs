using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class UIScript : MonoBehaviour
{
    [SerializeField] GameObject Source;

    [SerializeField] TextMeshProUGUI AmmoUI;
    [SerializeField] TextMeshProUGUI HealthUI;
    [SerializeField] TextMeshProUGUI PointsUI;

    private void Start(){
        AmmoUI.text = ("15");
        PointsUI.text = ("0");
    }


    public void UpdatePoints(int Points){
        PointsUI.text = Points.ToString();
    }

    public void UpdateAmmoTxt(int AmmoCount) {        
        AmmoUI.text = AmmoCount.ToString();
    }   
  
    
}
