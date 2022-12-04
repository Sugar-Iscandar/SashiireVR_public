using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrabbable
{
    void OnGrabbed(Rigidbody rigidbody);
    void OnReleased();
}
