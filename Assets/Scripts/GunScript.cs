using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour {
    // Start is called before the first frame update
    public GunClass gun;
    [SerializeField] int Damage;
    [SerializeField] float RateOfFire;
    [SerializeField] AudioClip Sound;
    [SerializeField] int ammo;
    [SerializeField] int currentAmmo;
    [SerializeField] bool Automatic;
    [SerializeField] float ReloadTime;
    public bool Reloading;

    
    private void Start()  {        
        gun = new GunClass(Damage,RateOfFire,Sound,ammo,Automatic,ReloadTime,currentAmmo);       
    
    }

    //Gun manages reload
    IEnumerator Reload() {        
        Debug.Log("RELOADING");
        Reloading = true;
        yield return new WaitForSeconds(gun.ReloadTime);
        gun.currentAmmo = gun.Ammo;
        Reloading = false;        
    }




}


