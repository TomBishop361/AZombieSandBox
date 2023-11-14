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
    Animation Anim;

    
    private void Start()  {        
        gun = new GunClass(Damage,RateOfFire,Sound,ammo,Automatic,ReloadTime,currentAmmo);
        //Anim = GetComponent<Animator>();


    }

    private void OnEnable()
    {
        Reloading = false;
    }

    public void Fire()
    {
        GetComponent<Animator>().SetTrigger("Fire");
    }

    public void SwapWeapon(){
        GetComponent<Animator>().SetTrigger("Swap");

    }

    //Gun manages reload
    IEnumerator Reload() {
        if (!Reloading){
            Reloading = true;
            if (gun.currentAmmo == 0)
            {
                GetComponent<Animator>().SetTrigger("ReloadNoAmmo");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Reload");
            }

            yield return new WaitForSeconds(gun.ReloadTime);
            gun.currentAmmo = gun.Ammo;
            Reloading = false;
        }
    }


}


