using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEnemyRoom : DungeonRoom
{

    public TreasureChest[] chests;

    public override void OnTriggerEnter2D(Collider2D other)
    {

        base.OnTriggerEnter2D(other);

        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
        {

            for (int i = 0; i < chests.Length; i++)
            {

                if (!chests[i].isOpen)
                {

                    ChangeActivation(chests[i], false);

                }

                else
                {

                    ChangeActivation(chests[i], true);

                }

            }

            if (enemies != null)
            {

                CloseDoors();

            }

        }

    }

    public override void OnTriggerExit2D(Collider2D other)
    {

        base.OnTriggerExit2D(other);

        if (other.gameObject.CompareTag("Player") && !other.isTrigger)

        {

            for (int i = 0; i < chests.Length; i++)
            {

                ChangeActivation(chests[i], false);

            }

        }

    }

    public void CheckEnemies()
    {

        for (int i = 0; i < enemies.Length; i++)
        {

            if (enemies[i] != null)
            {

                if (enemies[i].gameObject.activeInHierarchy)
                {

                    return;

                }

            }
            else
            { continue; }

        }

        for(int i = 0; i < chests.Length; i++)
        {

            ChangeActivation(chests[i], true);

        }

        if (doors != null)
        {

            OpenDoors();

        }

        return;

    }

    public void CloseDoors()
    {

        for (int i = 0; i < doors.Length; i++)
        {

            doors[i].Close();

        }

    }

    public void OpenDoors()
    {

        for (int i = 0; i < doors.Length; i++)
        {

            doors[i].Open();

        }

    }

}
