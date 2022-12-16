using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// class for have object always face camera , in this case for canvas in particle to always face camera
/// </summary>
public class FollowCamera : MonoBehaviour
{
  

    /// <summary>
    ///  have transform always face camera 
    /// </summary>
    void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}
