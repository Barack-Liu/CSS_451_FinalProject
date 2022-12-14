using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public int difficulty; // Required total for success
    public int d; //d4, d6, d8, d10, d20 etc.
    public int dCount; // How many dice are used?
    //[SerializeField] private int mod; // Relevant skill modifier

    //public float xpGain; // Awarded xp for success

    private SkillCheck roll;

    // Roll for attack, uses relevant skill modifier and gets difficulty dynamically from enemy stats
    public bool AttackRoll(int mod)
    {
        difficulty = gameObject.GetComponent<StatSheet>().DEF;
        roll = new SkillCheck(d, dCount, mod);
        bool success = roll.CheckSuccess(difficulty, roll.Roll());
        return success;
    }

    public void PhysicalAttack(float x, float y, float z)
    {
        // do scenenode transformation
        StartCoroutine(enemyAttackRoutine(x,y,z));
    }

    IEnumerator enemyAttackRoutine(float x, float y, float z)
    {
        // Get enemy's position;
        Vector3 enemy;
        enemy = transform.localPosition;

        //Get general's position;
        Vector3 general;
        general = new Vector3 (x, y, z);

        //Move the enemy to the general the postion;
        transform.localPosition = general;

        //Wait for 1 second;
        yield return new WaitForSeconds(0.5f);

        //Move the enemy to the orignal position;
        transform.localPosition = enemy;
        
    }
}
