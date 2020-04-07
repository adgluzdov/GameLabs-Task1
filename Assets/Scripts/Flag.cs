using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    public int widthCount = 2;
    public int lengthCount = 100;
    public float scroll = 0;

    public static Mesh Plane(Vector3 origin, Vector3 width, Vector3 length, int widthCount, int lengthCount, float scroll)
    {
        var normal = Vector3.Cross(length, width).normalized;
        Vector3[] vertices = new Vector3[widthCount * lengthCount];
        int[] triangles = new int[(widthCount - 1) * (lengthCount - 1) * 2 * 3];
        Vector2[] uv = new Vector2[widthCount * lengthCount];
        for (var w = 0; w < widthCount; w++)
        {
            for (var l = 0; l < lengthCount; l++)
            {
                vertices[w * lengthCount + l] = origin + (width * w / (widthCount - 1)) + (length * l / (lengthCount - 1)) + normal * Mathf.Sin(length.magnitude * (float)l / (lengthCount - 1) + scroll);
                uv[w * lengthCount + l] = new Vector2((float)l / (lengthCount - 1), (float)w / (widthCount - 1));
            }
        }
        for (int q = 0, i = 0; q < (widthCount - 1) * (lengthCount - 1); q++) {
            int anchor = q + q / (lengthCount - 1);
            triangles[i] = anchor;
            i++;
            triangles[i] = anchor + 1;
            i++;
            triangles[i] = anchor + lengthCount;
            i++;
            triangles[i] = anchor + 1;
            i++;
            triangles[i] = anchor + 1 + lengthCount;
            i++;
            triangles[i] = anchor + lengthCount;
            i++;
        }
        var mesh = new Mesh
        {
            vertices = vertices,
            triangles = triangles,
            uv = uv,
        };
        mesh.RecalculateNormals();
        return mesh;
    }

    public void Update()
    {
        GetComponent<MeshFilter>().mesh = Plane(Vector3.zero, new Vector3(5, 0, 0), new Vector3(0, 0, 10), widthCount, lengthCount, scroll);
    }

}
