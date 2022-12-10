using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour
{
    public SceneNode root;
    public float HP;
    public float ATK;
    public float DEF;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Matrix4x4 i = Matrix4x4.identity;
        root.CompositeXform(ref i);
    }

    public void PhysicalAttack(string bodypart)
    {
        // do scenenode transformation
    }

    public void PhysicalDefense(string bodypart)
    {
        // do scenenode transformation
    }

    public void MagicAttack()
    {
        // do scenenode transformation
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
