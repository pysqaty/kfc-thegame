using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Implementation based on Sebastian Lague's "Coding Adventure: Portals"
public class Portal : MonoBehaviour
{
    #region Initialisation
    void Awake()
    {
        travellers = new List<PortalTraveller>();

        // TODO: (visuals)
        playerCam = Camera.main;
        //portalCam = GetComponentInChildren<Camera>();
        //portalCam.enabled = false;
    }
    #endregion

    #region Teleportation Logic

    private void Teleport(PortalTraveller trav)
    {
        linkedPortal.travellers.Add(trav);

        var m = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * trav.transform.localToWorldMatrix;
        trav.Teleport(transform, linkedPortal.transform, m.GetColumn(3), m.rotation);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Enter {other.name}");
        var trav = other.GetComponent<PlayerPortalTraveller>();
        if (trav && travellers.Contains(trav) == false)
        {
            Teleport(trav);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log($"Exit {other.name}");
        var trav = other.GetComponent<PlayerPortalTraveller>();
        if (trav && travellers.Contains(trav))
        {
            travellers.Remove(trav);
        }
    }
    #endregion

    #region Visuals
    public void SetEvelopeStartColour(Color col1, Color col2) 
	{
		var col = envelopePS.colorBySpeed;
		col.enabled = true;

		Gradient grad = new Gradient();
		grad.SetKeys( 
			new GradientColorKey[] {
				new GradientColorKey(col2, 0.0f),
				new GradientColorKey(col1, 1.0f) 
			},
			new GradientAlphaKey[] {
				new GradientAlphaKey(0.3f, 0.0f),
				new GradientAlphaKey(0.8f, 1.0f) 
			} 
		);

		col.color = grad;
	}
	
	void CreateViewTexture()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }
            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            portalCam.targetTexture = viewTexture;
            linkedPortal.screen.material.SetTexture("_MainTex", viewTexture);
        }
    }

    // TODO: visuals disabled for now
    // PTODO: has to be called before playerCam?
    public void Render()
    {
        //screen.enabled = false;
        //CreateViewTexture();
        //
        //var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;
        //portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
        //
        //portalCam.Render();
    }
    #endregion

    #region Variables
    public Portal linkedPortal;
    public MeshRenderer screen;
    public ParticleSystem envelopePS;
    private Camera playerCam;
    private Camera portalCam = null; // TODO required for visuals
    private RenderTexture viewTexture;
    private List<PortalTraveller> travellers;
    #endregion
}