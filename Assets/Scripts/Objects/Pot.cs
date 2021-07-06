using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Breakable
{

    private Animator anim;
    public LootTable thisLoot;

    // Start is called before the first frame update
    void Start()
    {

        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Smash()
    {

        anim.SetBool("smash", true);

        StartCoroutine(BreakCo());

    }

    IEnumerator BreakCo()
    {

        yield return new WaitForSeconds(.3f);
        MakeLoot();

        if (canRespawn)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private void MakeLoot()
    {

        if (thisLoot != null)
        {

            PowerUp current = thisLoot.LootPowerUp();

            if (current != null)
            {

                Instantiate(current.gameObject, transform.position, Quaternion.identity);

            }

            else { return; }

        }

    }

}
