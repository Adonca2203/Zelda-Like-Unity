using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{

    public bool active;
    public BoolValue storedValue;
    public Sprite activeSprite;
    private SpriteRenderer sprite;
    public Door thisDoor;

    // Start is called before the first frame update
    void Start()
    {

        sprite = GetComponent<SpriteRenderer>();
        active = storedValue.RunTimeValue;

        if(active)
        {

            ActivateSwitch();

        }

    }

    public void ActivateSwitch()
    {

        active = true;
        storedValue.RunTimeValue = active;
        thisDoor.Open();
        sprite.sprite = activeSprite;

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.CompareTag("Player") && !other.isTrigger)
        {

            ActivateSwitch();

        }

    }

}
