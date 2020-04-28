using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject boss;
    CharacterState stats;
    Image im;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        boss = GameObject.Find("Boss(clone)");
        stats = boss.GetComponent<CharacterState>();
        im = GetComponent<Image>();

    }
   

    // Update is called once per frame
    void Update()
    {
        im.fillAmount = stats.getHealthRate();
    }
}
