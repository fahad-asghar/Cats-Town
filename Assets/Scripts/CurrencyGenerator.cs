using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Numerics;
using System;
using System.Runtime.InteropServices;

public class CurrencyGenerator : MonoBehaviour
{

    [SerializeField] public string buyingPrice;
    [SerializeField] public string currencyPerSecond;
    
    private void Start()
    {
        InvokeRepeating("GenerateCurrency", 1, 1);
    }

    private void GenerateCurrency()
    {
        UnityEngine.Vector2 originalScale = transform.localScale;

        transform.DOScale(new UnityEngine.Vector2(transform.localScale.x + 3, transform.localScale.y + 3), 0.2f).OnComplete(delegate ()
        {
            transform.DOScale(originalScale, 0.1f).OnComplete(delegate() {
                transform.localScale = originalScale;
            });
        });

        if (!GameHandler.instance.doubleMoneyEnabled)
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

            transform.GetChild(0).GetChild(1).GetComponent<Animator>().enabled = true;
            transform.GetChild(0).GetChild(1).GetComponent<Animator>().Play("FloatingCurrency", -1, 0);

            GameHandler.instance.UpdateCurrency(BigInteger.Parse(currencyPerSecond));
        }
        else
        {
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true);

            transform.GetChild(0).GetChild(2).GetComponent<Animator>().enabled = true;
            transform.GetChild(0).GetChild(2).GetComponent<Animator>().Play("FloatingCurrency", -1, 0);

            GameHandler.instance.UpdateCurrency(BigInteger.Parse(currencyPerSecond) * 2);
        }
    }
}
