using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// interface for implementing particle
/// </summary>
public interface IParticle 
{

    /// <summary>
    /// share particles charge
    /// </summary>
    /// <returns>float charge </returns>
    public float GetCharge();
    public void Grab(bool grabbed);

    public bool IsGrabbed();

}
