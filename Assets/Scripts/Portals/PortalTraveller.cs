using UnityEngine;

// Implementation based on Sebastian Lague's "Coding Adventure: Portals"
public abstract class PortalTraveller : MonoBehaviour
{
    public abstract void Teleport(Transform startPortal, Transform dstPortal, Vector3 pos, Quaternion rot);
}
