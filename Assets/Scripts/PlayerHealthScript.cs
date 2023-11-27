using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] Volume vol;
    private Vignette vg;
    [SerializeField] GameObject UI;
    int health;
    [SerializeField] UnityEvent Death;

    // Start is called before the first frame update
    void Start() { 
        health = 100;
        vol.profile.TryGet<Vignette>(out vg);
    }


    IEnumerator DamageEfct(){
        vg.color.value = new Color(1f, 0f, 0f);
        yield return new WaitForSeconds(0.75f);
        vg.color.value = new Color(0f, 0f, 0f);


    }
    

    public void Attacked(){
        health -= 25;
        UI.GetComponent<UIScript>().UpdateHealthTxt(health);
        StartCoroutine("DamageEfct");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Death.Invoke();
        }
    }
}
