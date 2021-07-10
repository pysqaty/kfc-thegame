using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;

public static class BakeGroundCollisionMeshEditor
{
    [MenuItem("KFC/Bake Ground Collision Mesh")]
    public static void BakeCollisionMesh()
    {
        var tmpGo = new GameObject();
        var nms = tmpGo.AddComponent<NavMeshSurface>();
        nms.agentTypeID = GetAgentTypeId("Dummy", out int idx);
        nms.layerMask = LayerMask.GetMask("Environment");
        nms.collectObjects = CollectObjects.All;
        nms.useGUILayout = false;
        nms.runInEditMode = true;
        var baker = tmpGo.AddComponent<GroundCollisionMeshBaker>();
        baker.StartCoroutine("BakeMesh");
    }

    private static int GetAgentTypeId(string name, out int index)
    {
        var count = NavMesh.GetSettingsCount();
        for (var i = 0; i < count; i++)
        {
            var id = NavMesh.GetSettingsByIndex(i).agentTypeID;
            var agentName = NavMesh.GetSettingsNameFromID(id);
            if(name == agentName)
            {
                index = i;
                return id;
            }
        }
        index = -1;
        return -1;
    }
}
