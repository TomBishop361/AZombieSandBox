using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour {
    GameObject UI;
    public GunClass gun;
    [HideInInspector]public bool Reloading;
    Animator Anim;

    private void Start(){
        gun.currentAmmo = gun.Ammo;   
    }

    private void OnEnable(){
        UI = GameObject.FindGameObjectWithTag("UI");
        Anim = GetComponent<Animator>();
        Reloading = false;
    }

    public void Fire(){       
        if(gun.Ammo == 0){
            Anim.SetTrigger("Empty");
        }else
        {
            Anim.SetTrigger("Fire");
        }
    }

    public void SwapWeapon(){
        Anim.SetTrigger("Swap");
    }

    //Gun manages reload
    IEnumerator Reload() {
        if (!Reloading){
            Reloading = true;
            if (gun.currentAmmo == 0)
            {
                Anim.SetTrigger("ReloadNoAmmo");
            }
            else
            {
                Anim.SetTrigger("Reload");
            }

            yield return new WaitForSeconds(gun.ReloadTime);
            gun.currentAmmo = gun.Ammo;
            UI.GetComponent<UIScript>().UpdateAmmoTxt(gun.currentAmmo);
            Reloading = false;
        }
    }       

}


