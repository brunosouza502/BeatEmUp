using UnityEngine;
using System;


[Serializable]
public class Combo
{
    public Hit[] hits; // An array of Hit objects that make up the combo.

    [HideInInspector]
    public int currentHit;
    public string attackGroup;

    public void Advance()//Index control
    {
        currentHit++;

        if (currentHit >= hits.Length)
        {
            currentHit = 0;
        }
    }

    public void Reset()
    {
        currentHit = 0;
    }
}


[Serializable]
public class Hit
{
    public string attackType;
    public string triggerAnim;// The name of the animation trigger for this hit.
    public int damage;
    public string button; // The button that needs to be pressed to execute this hit.
}