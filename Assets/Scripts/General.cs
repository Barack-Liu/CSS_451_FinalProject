using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    public SceneNode root;

    public GameObject magic;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(magic != null);
        Camera.main.GetComponent<CamController>().target = transform;
        GameObject.Find("SmallViewCamera").GetComponent<SmallViewCamera>().general = root;
        GameObject.Find("Canvas").GetComponent<FightMenu>().playerObject = gameObject;
        GameObject.Find("Canvas").GetComponent<FightMenu>().battleSceneRef = transform.GetComponent<BattleScene>();
        GameObject.Find("Canvas").transform.Find("HealthBar").GetComponent<HealthBarScript>().playerCube = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 i = Matrix4x4.identity;
        root.CompositeXform(ref i);
    }

    public void PhysicalAttack()
    {
        // do scenenode transformation
        StartCoroutine(attackRoutine());
    }

    public void PhysicalDefend()
    {
        // do scenenode transformation
        StartCoroutine(defendRoutine());
    }

    IEnumerator attackRoutine()
    {
        // turn right arm
        Transform body = transform.Find("Body");
        Transform rightarm = body.Find("RightArm");
        Vector3 nRightarm = rightarm.up;
        Quaternion q = Quaternion.AngleAxis(-70f, nRightarm);
        float distance = rightarm.Find("Geom").Find("Arm").localScale.y;
        Vector3 pivot = rightarm.localPosition + nRightarm * distance / 2;
        rightarm.localPosition = q * (rightarm.localPosition - pivot) + pivot;
        rightarm.localRotation = q * rightarm.localRotation;
        // turn right hand
        Transform rightsmallarm = rightarm.Find("Arm");
        Transform righthand = rightsmallarm.Find("Hand");
        Vector3 nRighthand = righthand.right;
        q = Quaternion.AngleAxis(75f, nRighthand);
        pivot = righthand.localPosition;
        righthand.localPosition = q * (righthand.localPosition - pivot) + pivot;
        righthand.localRotation = q * righthand.localRotation;
        yield return new WaitForSeconds(1);
        body.GetComponent<SceneNode>().Reset();
    }

    IEnumerator defendRoutine()
    {
        // turn left arm
        Transform body = transform.Find("Body");
        Transform rightarm = body.Find("LeftArm");
        Vector3 nRightarm = rightarm.up;
        Quaternion q = Quaternion.AngleAxis(90f, nRightarm);
        float distance = rightarm.Find("Geom").Find("Arm").localScale.y;
        Vector3 pivot = rightarm.localPosition + nRightarm * distance / 2;
        rightarm.localPosition = q * (rightarm.localPosition - pivot) + pivot;
        rightarm.localRotation = q * rightarm.localRotation;
        // turn left small arm
        Transform rightsmallarm = rightarm.Find("Arm");
        Vector3 nRighrsmallarm = rightsmallarm.forward;
        q = Quaternion.AngleAxis(90f, nRighrsmallarm);
        distance = rightsmallarm.Find("Geom").Find("Arm").localScale.y;
        pivot = rightsmallarm.localPosition + rightsmallarm.forward * distance / 2;
        rightsmallarm.localPosition = q * (rightsmallarm.localPosition - pivot) + pivot;
        rightsmallarm.localRotation = q * rightsmallarm.localRotation;
        yield return new WaitForSeconds(1);
        body.GetComponent<SceneNode>().Reset();
    }

    public void MagicAttack()
    {
        // do scenenode transformation
        StartCoroutine(magicAttackRoutine());
    }

    IEnumerator magicAttackRoutine()
    {
        // turn right arm
        Transform body = transform.Find("Body");
        Transform rightarm = body.Find("RightArm");
        Vector3 nRightarm = rightarm.up;
        Quaternion q = Quaternion.AngleAxis(-90f, nRightarm);
        float distance = rightarm.Find("Geom").Find("Arm").localScale.y;
        Vector3 pivot = rightarm.localPosition + nRightarm * distance / 2;
        rightarm.localPosition = q * (rightarm.localPosition - pivot) + pivot;
        rightarm.localRotation = q * rightarm.localRotation;
        // turn right small arm
        Transform rightsmallarm = rightarm.Find("Arm");
        Vector3 nRighrsmallarm = rightsmallarm.forward;
        q = Quaternion.AngleAxis(90f, nRighrsmallarm);
        distance = rightsmallarm.Find("Geom").Find("Arm").localScale.y;
        pivot = rightsmallarm.localPosition + rightsmallarm.forward * distance / 2;
        rightsmallarm.localPosition = q * (rightsmallarm.localPosition - pivot) + pivot;
        rightsmallarm.localRotation = q * rightsmallarm.localRotation;
        Instantiate(magic);
        BattleScene bs = transform.GetComponent<BattleScene>();
        magic.GetComponent<Fireball>().SetEndPts(CalculateSpearHeadPosition(), bs.otherCube.transform.localPosition);
        yield return new WaitForSeconds(1);
        body.GetComponent<SceneNode>().Reset();
    }

    public Vector3 CalculateSpearHeadPosition()
    {
        SceneNode body = root.transform.Find("Body").GetComponent<SceneNode>();
        SceneNode rightarm = body.transform.Find("RightArm").GetComponent<SceneNode>();
        SceneNode arm = rightarm.transform.Find("Arm").GetComponent<SceneNode>();
        SceneNode hand = arm.transform.Find("Hand").GetComponent<SceneNode>();
        Matrix4x4 basetrs = Matrix4x4.TRS(root.transform.localPosition + root.NodeOrigin, root.transform.localRotation, root.transform.localScale);
        Matrix4x4 bodytrs = Matrix4x4.TRS(body.transform.localPosition + body.NodeOrigin, body.transform.localRotation, body.transform.localScale);
        Matrix4x4 rightarmtrs = Matrix4x4.TRS(rightarm.transform.localPosition + rightarm.NodeOrigin, rightarm.transform.localRotation, rightarm.transform.localScale);
        Matrix4x4 armtrs = Matrix4x4.TRS(arm.transform.localPosition + arm.NodeOrigin, arm.transform.localRotation, arm.transform.localScale);
        Matrix4x4 handtrs = Matrix4x4.TRS(hand.transform.localPosition + hand.NodeOrigin, hand.transform.localRotation, hand.transform.localScale);
        Matrix4x4 m = basetrs * bodytrs * rightarmtrs * armtrs * handtrs;
        Vector3 position = m.GetColumn(3);
        return position + hand.transform.Find("Geom").Find("Spear").up * -4f;
    }
}
