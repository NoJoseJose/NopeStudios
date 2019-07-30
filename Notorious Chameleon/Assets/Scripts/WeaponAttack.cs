using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public GameObject weapon;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = weapon.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool fire = Input.GetButtonDown("FireRB");
        animator.SetBool("Attack", fire);
    }
}
