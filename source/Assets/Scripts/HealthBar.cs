using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    Image healthBar;
    CharacterState characterState;
    void Start()
    {
        healthBar = GetComponent<Image>();
        characterState = transform.parent.parent.gameObject.GetComponent<CharacterState>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = characterState.getHealthRate();
    }
}
