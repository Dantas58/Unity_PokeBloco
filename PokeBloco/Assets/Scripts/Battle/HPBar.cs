using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{

    [SerializeField] GameObject health;

    public void SetHP(float hpNormalized)
    {
        health.transform.localScale = new Vector3(hpNormalized, 1f);

        health.GetComponent<Image>().color = Color.green;
    }

    public IEnumerator SetHPSmooth(float newHP, float maxHP)
    {
        float curHP = health.transform.localScale.x;
        float changeAmt = curHP - newHP;

        while(curHP - newHP > Mathf.Epsilon)
        {
            curHP -= changeAmt * Time.deltaTime;
            SetHP(curHP);

            if (curHP <= 0.15f * maxHP)
                health.GetComponent<Image>().color = Color.red;

            else if (curHP <= 0.5f * maxHP)
                health.GetComponent<Image>().color = Color.yellow;

            yield return null;
        }

        SetHP(newHP);
    }

}
