using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallViewCamera : MonoBehaviour
{
    public SceneNode general = null;
    private SceneNode body = null;
    private SceneNode neck = null;
    private SceneNode head = null;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(general != null);
        body = general.transform.Find("Body").GetComponent<SceneNode>();
        neck = body.transform.Find("Neck").GetComponent<SceneNode>();
        head = neck.transform.Find("Head").GetComponent<SceneNode>();
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 basetrs = Matrix4x4.TRS(general.transform.localPosition + general.NodeOrigin, general.transform.localRotation, general.transform.localScale);
        Matrix4x4 bodytrs = Matrix4x4.TRS(body.transform.localPosition + body.NodeOrigin, body.transform.localRotation, body.transform.localScale);
        Matrix4x4 necktrs = Matrix4x4.TRS(neck.transform.localPosition + neck.NodeOrigin, neck.transform.localRotation, neck.transform.localScale);
        Matrix4x4 headtrs = Matrix4x4.TRS(head.transform.localPosition + head.NodeOrigin, head.transform.localRotation, head.transform.localScale);
        Matrix4x4 m = basetrs * bodytrs * necktrs * headtrs;
        Vector3 position = m.GetColumn(3);
        Quaternion rotation = Quaternion.LookRotation(m.GetColumn(2), m.GetColumn(1));
        transform.localPosition = position;
        transform.localRotation = rotation;
        Quaternion q = Quaternion.AngleAxis(90f, transform.up);
        transform.localRotation = q * transform.localRotation;
    }
}
