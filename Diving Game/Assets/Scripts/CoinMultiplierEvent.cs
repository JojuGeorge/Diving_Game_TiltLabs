using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMultiplier : MonoBehaviour
{

    private float _dotProd;

    public enum DiveScoreValue {NULL=0, BAD=1, OKAY=2, PERFECT=3}
    public DiveScoreValue coinMulEnum;

    public delegate void DiveAng(string value, float multiplyValue);
    public static event DiveAng DiveAngScore;

    void Start()
    {
        
    }

    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Water") { 
            _dotProd = DiveAngleCalc();
            coinMulEnum = ScoreCalculator(_dotProd);

            DiveAngScore(coinMulEnum.ToString(), (int)coinMulEnum);     //event


         //   Debug.Log(" dot prod = " + _dotProd);
       //     Debug.Log("coin mul score = " + coinMulEnum.ToString() + " ___ " + (int)coinMulEnum);

        }

    }


    private float DiveAngleCalc() {
        Vector3 worldUp = Vector3.up;
        Vector3 playerForward = transform.forward;
        return  Vector3.Dot(worldUp, playerForward);
    }

    private DiveScoreValue ScoreCalculator(float dotProdValue) {

        float dotProd = Mathf.Abs(dotProdValue);

        if (dotProd >= 0 && dotProd <= 0.3)
        {
            return DiveScoreValue.PERFECT;
        }
        else if (dotProd > 0.3 && dotProd <= 0.7)
        {
            return DiveScoreValue.OKAY;
        }
        else if (dotProd > 0.7 && dotProd <= 1)
        {
            return DiveScoreValue.BAD;
        }
        else {
            return DiveScoreValue.NULL;
        } 

    }

}
