using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : MonoBehaviour
{
    public TheWorld theWorld;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        BattleHandler();
    }

    void BattleHandler()
    {
        /*
        if (waitforattack) {
            if (attacker clicked attack button) {
                already attacked
            }
        }
        if (waitfordefense) {
            if (defenser clicked defense button) {
                already defensed
            }
        }
        if (already attacked && already defensed) {
            theWorld.Battle();
            switch attack and defense
            waitforattack
            waitfordefense
        }
        */
    }
}
