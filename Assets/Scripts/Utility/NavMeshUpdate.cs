using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshUpdate : MonoBehaviour
{
    private NavMeshSurface[] surfaces;
    public bool IsDirty;

    // Start is called before the first frame update
    void Start()
    {
        surfaces = GetComponentsInChildren<NavMeshSurface>();
        //RecalculateNavMesh();

        IsDirty = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsDirty)
        {
            RecalculateNavMesh();
            IsDirty = false;
        }
    }

    private void RecalculateNavMesh()
    {
        foreach (var surf in surfaces)
        {
            surf.BuildNavMesh();
        }
    }
}
