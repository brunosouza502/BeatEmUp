using UnityEngine;
using System;


[Serializable]
public class Combo
{
    public Hit[] hits; // An array of Hit objects that make up the combo.
    //public string inputButton;

    [HideInInspector]
    public int currentHit = 0;
    public string attackGroup;

    public void Advance()//Index control
    {
        Debug.Log("Advance() BEFORE = " + currentHit);

        currentHit++;

        if (currentHit >= hits.Length)
        {
            currentHit = 0;
        }

        Debug.Log("Advance() AFTER = " + currentHit);
    }

    public void Reset()
    {
        currentHit = 0;
        Debug.Log("Reset() chamado em " + attackGroup);
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