using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.AI;
using UnityEngine;
using UnityEngine.AI;

public class GroundCollisionMeshBaker : MonoBehaviour
{
    [ExecuteInEditMode]
    public IEnumerator BakeMesh()
    {
        NavMeshSurface surface = GetComponent<NavMeshSurface>();
        var targets = new UnityEngine.Object[] { surface };
        NavMeshAssetManager.instance.StartBakingSurfaces(targets);
        yield return new WaitWhile(() => NavMeshAssetManager.instance.IsSurfaceBaking(surface));

        var triangulation = NavMesh.CalculateTriangulation();
        Mesh mesh = new Mesh();
        mesh.vertices = triangulation.vertices;
        mesh.SetIndices(triangulation.indices, MeshTopology.Triangles, 0);

        AssetDatabase.CreateAsset(mesh, "Assets/Models/Collisions/GroundCollider.asset");
        AssetDatabase.SaveAssets();
        NavMeshAssetManager.instance.ClearSurfaces(targets);

        DestroyImmediate(gameObject);
        yield return false;
    }
}
