using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PortalManager : MonoBehaviour
{
    void Start()
    {
		offMeshLinks = new List<OffMeshLink>();
		UpdateManagedPortals();
	}
	
	public void ShufflePortalLinks() 
	{				
		Assets.Scripts.Utility.CollectionsUtils.Shuffle(portals);
		
		for (int i = 0; i < portals.Count; i += 2) 
		{
			portals[i].linkedPortal = portals[i + 1];
			portals[i + 1].linkedPortal = portals[i];
			
			var col1 = Color.HSVToRGB((i * 1.0f)/(portals.Count), 0.85f, 1.0f);
			var col2 = Color.HSVToRGB((i * 1.0f)/(portals.Count), 0.6f, 1.0f);
			
			portals[i].SetEvelopeStartColour(col1, col2);
			portals[i+1].SetEvelopeStartColour(col1, col2);
			
			var link = offMeshLinks[(i/2)];
			link.startTransform = portals[i].transform;
			link.endTransform = portals[i+1].transform;
			link.biDirectional = true;
			link.activated = true;
			link.costOverride = portalLinkCost;
			link.UpdatePositions();
		}
	}
	
	// Should be used whenever a portal is added or removed from THE GameObject.
	// Excluding Start()-phase where the function is called by default.
	public void UpdateManagedPortals()
	{
		portals = getChildPortals();
		Debug.Assert(portals.Count % 2 == 0, "Num of portals has to be pairable");
		ResizeLinks(portals.Count / 2);
	}
	
	#pragma region Private Utils
	
	private List<Portal> getChildPortals() 
	{
		var buff = new List<Portal>();
		
		foreach (Transform child in this.transform) 
		{
			Portal portal = child.gameObject.GetComponent<Portal>(); 
			if (portal != null) 
			{
				buff.Add(portal);
			}
		}
		
		return buff;
	}
	
	private void ResizeLinks(int size)
	{
		if (offMeshLinks.Count < size) 
		{
			for (int i = offMeshLinks.Count; i < size; i++) 
			{
				offMeshLinks.Add(this.gameObject.AddComponent(typeof(OffMeshLink)) as OffMeshLink);
			}
		}
		else if (offMeshLinks.Count > size) 
		{
			offMeshLinks.RemoveRange(size, offMeshLinks.Count);
		}
	}
	
	#pragma endregion
	
	#pragma region Variables
	
	List<Portal> portals;
	List<OffMeshLink> offMeshLinks;
	
	const float portalLinkCost = 1e-10f;
	
	#pragma endregion
}
