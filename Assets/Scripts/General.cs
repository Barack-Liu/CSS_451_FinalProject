using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    public SceneNode root;
    public float HP;
    public float ATK;
    public float DEF;

    public GameObject magic;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(magic != null);
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
        magic.GetComponent<Fireball>().SetEndPts(bs.playerCube.transform.localPosition, bs.otherCube.transform.localPosition);
        yield return new WaitForSeconds(1);
        body.GetComponent<SceneNode>().Reset();
    }

    public void MagicDefense()
    {
        // do scenenode transformation
    }

    public void ReceiveDamage(float damage)
    {
        HP -= damage;
        // need to check death
    }
}
