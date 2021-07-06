using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    [Header("Current Room Objects")]
    public EnemyAI[] enemies;
    public Breakable[] breakables;
    public PowerUp[] pickUps;
    [Header("Room Camera")]
    public GameObject roomCamera;

    public virtual void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.CompareTag("Player") && !other.isTrigger)
        {

            //Activate all objects

            if (enemies.Length != 0)
            {

                for (int i = 0; i < enemies.Length; i++)
                {

                    if (enemies[i] == null)
                    {

                        continue;

                    }

                    ChangeActivation(enemies[i], true);

                }

            }

            if (breakables.Length != 0)

            {

                for (int i = 0; i < breakables.Length; i++)
                {

                    if (breakables[i] == null)
                    {

                        continue;

                    }

                    ChangeActivation(breakables[i], true);

                }

            }

            if (pickUps.Length != 0)
            {

                for (int i = 0; i < pickUps.Length; i++)
                {

                    if (pickUps[i] == null)
                    {

                        continue;

                    }

                    ChangeActivation(pickUps[i], true);

                }

            }

            roomCamera.SetActive(true);

        }

    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {

            roomCamera.SetActive(false);

            //Dectivate all objects

            if (enemies.Length != 0)

            {
                for (int i = 0; i < enemies.Length; i++)
                {

                    if (enemies[i] == null)
                    {

                        continue;

                    }

                    ChangeActivation(enemies[i], false);

                }

            }

            if (breakables.Length != 0)
            {

                for (int i = 0; i < breakables.Length; i++)
                {

                    if (breakables[i] == null)
                    {

                        continue;

                    }

                    ChangeActivation(breakables[i], false);

                }

            }

            if (pickUps.Length != 0)

            {

                for (int i = 0; i < pickUps.Length; i++)
                {

                    if (pickUps[i] == null)
                    {

                        continue;

                    }

                    ChangeActivation(pickUps[i], false);

                }

            }

        }

    }

    public void ChangeActivation(Component component, bool activation)
    {

        component.gameObject.SetActive(activation);

    }

}
