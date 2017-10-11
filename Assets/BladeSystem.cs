using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeSystem : MonoBehaviour
{
    GameObject CutObject;

    [SerializeField, Header("Material")]
    Material Mat;

    //[SerializeField,Header("Cutter")]
    //Cutter _cutter;

    private LineRenderer _lineRenderer;
    private Plane _plane;

    private Vector3 normal;
    private Vector3 position;

    //CutMesh cutMesh;

    Mesh mesh;

    private Vector3 StartPos, EndPos;

    void OnCollisionEnter(Collision collision)
    {
        CutObject = collision.gameObject;
        StartPos = collision.contacts[0].point;

        //対象オブジェクトのメッシュを取得
        //cutMesh = collision.gameObject.GetComponent<CutMesh>();

        mesh = CutObject.GetComponent<MeshFilter>().mesh;
    }

    void OnCollisionStay(Collision collision)
    {
        EndPos = collision.contacts[0].point;
    }

    void OnCollisionExit(Collision collision)
    {
        if(mesh != null)
        {
            Create();

            //オブジェクトカット
            Blade.Cut(CutObject, _plane, mesh, Mat);
        }
       
        //if (cutMesh != null)
        //{
        //    _cutter.Cut(_plane, cutMesh);
        //}
    }

    /// <summary>
    /// 平面を作成
    /// </summary>
    private void Create()
    {
        _plane = new Plane();

        position = (StartPos + EndPos) / 2;
        var p1 = StartPos - position;
        normal = (Quaternion.Euler(0f, 0f, 90f) * p1).normalized;
        _plane.SetNormalAndPosition(normal, position);

    }

    //void OnDrawGizmosSelected()
    //{
    //    float length = 10.0f;
    //    Gizmos.color = Color.blue;

    //    Gizmos.DrawLine(position, position + (normal * length));

    //}

    //public Plane Plane
    //{
    //    get
    //    {
    //        return _plane;
    //    }
    //}
}