using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;

public class ShootWeapon : MonoBehaviour
{
    //Pointer Variables    
    [SerializeField] private Camera Rcast;
    public GameObject[] LoadOut = new GameObject[2];
    int EquipWeapon = 2;

    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Awake()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
                
    }
        
    //Checks what input was pressed (1 or 2) then swaps weapon
    void OnSwapWeapon(InputValue Input){
        //(input.y Keyboard 1 / ScrollWheelUp = 1 | input.y KeyBoard 2 / ScrollWheelDown = -1)
        if (Input.Get<Vector2>().y < 0)
        {
            EquipWeapon = 2;            
            Debug.Log("Weapon 2 Equip");
            if (LoadOut[1] != null && LoadOut[0] != null){
                LoadOut[0].SetActive(false);
                LoadOut[1].SetActive(true);
            } 
        }
        if (Input.Get<Vector2>().y > 0)
        {
            EquipWeapon = 1;            
            Debug.Log("Weapon 1 Equip");
            if (LoadOut[1] != null && LoadOut[0] != null){
                LoadOut[1].SetActive(false);
                LoadOut[0].SetActive(true);
            }
        }
    }


    //Shoot and check target hit
    void OnShoot(){
        RaycastHit hit;
        if (Physics.Raycast(Rcast.transform.position, Rcast.transform.forward, out hit, 100f)){            
            Debug.DrawLine(Rcast.transform.position, hit.point, Color.red);
            Debug.Log("Shot " + hit.transform.name);
            //Call Function to check what weapon is equip and what noise to play
            if (hit.transform.CompareTag("Zombie"))
            {
                Debug.Log("Hit");
                //call damage calculation                
            }
        }
    }

}