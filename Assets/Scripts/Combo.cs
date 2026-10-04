using UnityEngine;
using System;


[Serializable]
public class Combo
{
    public Hit[] hits; // An array of Hit objects that make up the combo.

}


[Serializable]
public class Hit
{
    public string animationName;
    public string triggerAnim;// The name of the animation trigger for this hit.
    public int damage;
    public string button; // The button that needs to be pressed to execute this hit.
}