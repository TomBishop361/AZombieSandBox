using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.Windows;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.SocialPlatforms.Impl;

public class ShootWeapon : MonoBehaviour
{

    //Pointer Variables    
    [SerializeField] GameObject UI;
    [SerializeField] private InputActionReference playerShoot;
    [SerializeField] private Camera Rcast;
    public GameObject[] LoadOut = new GameObject[2];
    public int EquipWeapon = 1;
    float shotcounter;
    bool isFiring;
    bool CanFire = true;
    int PrevPoints;
    public int Points;

    private void Start(){
        Points = 0;
        PrevPoints = Points;
        Cursor.lockState = CursorLockMode.Locked;
    }
        
    //Checks what input was pressed (1 or 2) then swaps weapon
    void OnSwapWeapon(InputValue Input){        
        //(input.y Keyboard 1 / ScrollWheelUp = 1 | input.y KeyBoard 2 / ScrollWheelDown = -1)
        if (Input.Get<Vector2>().y < 0) {            
            if (LoadOut[1] != null && LoadOut[0] != null){
                LoadOut[0].SetActive(false);
                LoadOut[1].SetActive(true);
                EquipWeapon = 1;
                //Plays Draw Animation For Swapped to weapons
                LoadOut[EquipWeapon].GetComponent<Animator>().SetTrigger("Idle");
                LoadOut[EquipWeapon].GetComponent<GunScript>().SwapWeapon();
                //Fixes bug where player cant shoot Pistol When swapping weapon
                if (CanFire == false && isFiring == false){
                    StartCoroutine(RoF(LoadOut[EquipWeapon].GetComponent<GunScript>().gun.RateOfFire));
                }
            } 
        }
        if (Input.Get<Vector2>().y > 0){            
            if (LoadOut[1] != null && LoadOut[0] != null){
                EquipWeapon = 0;
                LoadOut[1].SetActive(false);
                LoadOut[0].SetActive(true);
                //Plays Draw Animation For Swapped to weapons
                LoadOut[EquipWeapon].GetComponent<GunScript>().SwapWeapon();
            }
        }
        
    }
       
    //Function called when Gun meets all shooting criteria
    public void Shoot() {        
        RaycastHit hit;
        if (Physics.Raycast(Rcast.transform.position, Rcast.transform.forward, out hit, 100f))
        {
            Debug.DrawLine(Rcast.transform.position, hit.point, Color.red);
            Debug.Log("Shot " + hit.transform.name);
            //Call Function to check what weapon is equip and what noise to play
            if (hit.transform.CompareTag("Zombie"))
            {
                hit.transform.GetComponent<ZombieScript>().Damaged(LoadOut[EquipWeapon].GetComponent<GunScript>().gun.Damage);
                Points += 10;
            }
        }
        LoadOut[EquipWeapon].GetComponent<GunScript>().gun.currentAmmo -= 1;
        UI.GetComponent<UIScript>().UpdateAmmoTxt(LoadOut[EquipWeapon].GetComponent<GunScript>().gun.currentAmmo);
        LoadOut[EquipWeapon].GetComponent<GunScript>().Fire();
    }

    //OnShoot is called when LMB is pressed
    void OnShoot(){
        //If Can fire & is NOT automatic & Has ammo        
        if (LoadOut[EquipWeapon].GetComponent<GunScript>().gun.Automatic == false && CanFire && LoadOut[EquipWeapon].GetComponent<GunScript>().gun.currentAmmo > 0
            && !LoadOut[EquipWeapon].GetComponent<GunScript>().Reloading){               
            Shoot();
            StartCoroutine(RoF(LoadOut[EquipWeapon].GetComponent<GunScript>().gun.RateOfFire));
        }
    }

    //Manages Guns rate of fire
    IEnumerator RoF(float FiredWeaponROF){
        //IsFiring Fixes bug where player can spam Swap weapon to increase Non-Automatic weapons ROF
        isFiring = true;
        CanFire = false;
        yield return new WaitForSeconds(FiredWeaponROF);
        CanFire = true;
        isFiring = false;
    }



    void OnInteract(){
        RaycastHit hit;
        if (Physics.Raycast(Rcast.transform.position, Rcast.transform.forward, out hit, 2f))
        {
            Debug.DrawLine(Rcast.transform.position, hit.point, Color.red);
            Debug.Log("Shot " + hit.transform.name);
            //Call Function to check what weapon is equip and what noise to play
            if (hit.transform.CompareTag("WallBuy"))
            {
                if (Points >= hit.transform.GetComponent<WallBuy>().Cost) {
                    Points -= hit.transform.GetComponent<WallBuy>().Cost;
                    if (LoadOut[0] == null)
                    {
                        GameObject NewGun = Instantiate(hit.transform.GetComponent<WallBuy>().Gun, Rcast.transform, false);
                        LoadOut[0] = NewGun;
                    }
                    else
                    {
                        Destroy(LoadOut[EquipWeapon]);
                        GameObject NewGun = Instantiate(hit.transform.GetComponent<WallBuy>().Gun, Rcast.transform, false);
                        LoadOut[EquipWeapon] = NewGun;
                        NewGun.SetActive(true);
                    }
                }
            }
        }
    }

    //When the player presses R call the reload subroutine in Equip Gun to reload weapon
    void OnReload(){
        LoadOut[EquipWeapon].GetComponent<GunScript>().StartCoroutine("Reload");

    }
    private void Update() {
        if(PrevPoints != Points)
        {
            PrevPoints = Points;
            UI.GetComponent<UIScript>().UpdatePoints(Points);
        }
        
    }

    private void FixedUpdate(){           
        //if player is shooting & Gun is automatic & gun has ammo & is not reloading
        if (playerShoot.action.ReadValue<float>() == 1 && LoadOut[EquipWeapon].GetComponent<GunScript>().gun.Automatic && LoadOut[EquipWeapon].GetComponent<GunScript>().gun.currentAmmo > 0 
            && !LoadOut[EquipWeapon].GetComponent<GunScript>().Reloading){
            shotcounter -= Time.deltaTime;
            if (shotcounter <= 0)
            {
                shotcounter = LoadOut[EquipWeapon].GetComponent<GunScript>().gun.RateOfFire;
                Shoot();
            }
            else
            {
                CanFire = false;
                shotcounter -= Time.deltaTime;
            }
        }       

    }

}