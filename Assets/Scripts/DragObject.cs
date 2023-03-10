using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Numerics;

public class DragObject : MonoBehaviour
{
  

    [HideInInspector] public UnityEngine.Vector3 startPosition;
    [HideInInspector] public UnityEngine.Vector3 startScale;

    [SerializeField] List<GameObject> catsList = new List<GameObject>();
    [SerializeField] GameObject catsSpawnEffect;

    private Collider2D col;
    private UnityEngine.Vector3 mousePosition;
    private UnityEngine.Vector3 offset;

    private static bool enableDrag;

    private static bool shootRay;
    private RaycastHit2D raycast;
    private GameObject temp;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<Canvas>().overrideSorting = true;
    }

    private void OnMouseDown()
    {
        if (!Navigation.instance.uiEnabled)
        {
            SoundManager.instance.playSound(SoundManager.instance.sound2);

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            offset = mousePosition - transform.position;
            enableDrag = true;

            ChangeSortingLayer();
            GetComponent<Collider2D>().enabled = false;
            shootRay = true;
        }
    }

    private void OnMouseDrag()
    {
        if (enableDrag)
        {
            UnityEngine.Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new UnityEngine.Vector3(mousePosition.x - offset.x, mousePosition.y - offset.y, transform.position.z);
        }
    }

    private void OnMouseUp()
    {
        if (enableDrag)
        {
            shootRay = false;
            enableDrag = false;

            if (temp != null)
            {
                if (temp.CompareTag("Platform"))
                {
                    if (temp.transform.childCount == 0)
                        ChangePosition();
                    else if (temp.transform.childCount > 0)
                    {
                        if (temp.transform.GetChild(0).CompareTag(gameObject.tag)
                            && temp.transform != gameObject.transform.parent
                            && temp.transform.GetChild(0).name != "52" && gameObject.name != "52")
                            Merge(true);
                        else if (!temp.transform.GetChild(0).CompareTag(gameObject.tag))
                            SwapPosition(true);
                        else
                            BackToOriginalPosition();
                    }
                }
                else if (temp.CompareTag(gameObject.tag)
                    && gameObject.name != "52"
                    && temp.gameObject.name != "52")
                    Merge(false);
                else if (temp.CompareTag("Sell"))
                    Sell();
                else if (!temp.CompareTag(gameObject.tag))
                    SwapPosition(false);
               
                else
                    BackToOriginalPosition();
            }
            else
                BackToOriginalPosition();
           
        }
    }

    private void Sell()
    {
        SoundManager.instance.playSound(SoundManager.instance.sound9);

        GetComponent<Collider2D>().enabled = false;

        BigInteger buyingPrice = BigInteger.Parse(GetComponent<CurrencyGenerator>().buyingPrice);
        BigInteger sellPrice = buyingPrice / 3;
        GameHandler.instance.UpdateCurrency(sellPrice);

        transform.parent = temp.transform;
        startPosition = new UnityEngine.Vector2(0, 0);
        DOTween.Kill(transform);


        transform.DOLocalMove(startPosition, 0.2f, false);
        transform.DOScale(new UnityEngine.Vector2(0, 0), 0.2f).OnComplete(delegate ()
        {
            DOTween.Kill(transform);
            Destroy(gameObject);
        });
    }

    private void Merge(bool isPlatform)
    {
        SoundManager.instance.playSound(SoundManager.instance.sound3);

        if (isPlatform)
        {
            if (temp.transform.GetChild(0).name == gameObject.name)
            {
                for (int i = 0; i < catsList.Count; i++)
                {
                    if (catsList[i].name == gameObject.name)
                    {
                        GameObject obj = Instantiate(catsList[++i], temp.transform.GetChild(0).transform.position, UnityEngine.Quaternion.identity);
                        obj.name = catsList[i].name;
                        obj.transform.parent = temp.transform.GetChild(0).transform.parent;
                        obj.GetComponent<DragObject>().startPosition = temp.transform.GetChild(0).transform.localPosition;

                        Instantiate(catsSpawnEffect, new UnityEngine.Vector3(temp.transform.GetChild(0).transform.position.x, temp.transform.GetChild(0).transform.position.y + 0.35f, 0), UnityEngine.Quaternion.identity);
                        Destroy(temp.transform.GetChild(0).gameObject);

                        GameHandler.instance.UpdateDailyTasks();
                        GameHandler.instance.UpdateAchievements(i);
                        Navigation.instance.NewCatUnlockPopUp(obj.GetComponent<SpriteRenderer>().sprite, obj.name, obj.tag, i);
                        Destroy(gameObject);
                    }
                }
            }
        }

        else
        {
            if (temp.name == gameObject.name)
            {
                for (int i = 0; i < catsList.Count; i++)
                {
                    if (catsList[i].name == gameObject.name)
                    {
                        GameObject obj = Instantiate(catsList[++i], temp.transform.position, UnityEngine.Quaternion.identity);
                        obj.name = catsList[i].name;
                        obj.transform.parent = temp.transform.parent;
                        obj.GetComponent<DragObject>().startPosition = temp.transform.localPosition;

                        Instantiate(catsSpawnEffect, new UnityEngine.Vector3(temp.transform.position.x, temp.transform.position.y + 0.35f, 0), UnityEngine.Quaternion.identity);
                        Destroy(temp.gameObject);

                        GameHandler.instance.UpdateAchievements(i);
                        Navigation.instance.NewCatUnlockPopUp(obj.GetComponent<SpriteRenderer>().sprite, obj.name, obj.tag, i);
                        Destroy(gameObject);
                    }
                }
            }
        }
    }

    private void ChangePosition()
    {
        SoundManager.instance.playSound(SoundManager.instance.sound5);

        transform.parent = temp.transform;
        startPosition = new UnityEngine.Vector2(0, 0);
        transform.DOLocalMove(startPosition, 0.1f, false).OnComplete(delegate ()
        {
            transform.GetComponent<Collider2D>().enabled = true;
            RevertSortingLayerToDefault();
        });
    }

    private void SwapPosition(bool isPlatform)
    {
        SoundManager.instance.playSound(SoundManager.instance.sound5);

        if (isPlatform)
        {
            Transform tempParent1 = transform.parent;
            Transform tempParent2 = temp.transform;

            Transform tempChild = temp.transform.GetChild(0);

            transform.parent = tempParent2;
            tempChild.parent = tempParent1;

            startPosition = new UnityEngine.Vector2(0, 0);
            transform.DOLocalMove(startPosition, 0.1f, false).OnComplete(delegate ()
            {
                transform.GetComponent<Collider2D>().enabled = true;
                RevertSortingLayerToDefault();
            });

            tempChild.GetComponent<Collider2D>().enabled = false;
            tempChild.GetComponent<DragObject>().startPosition = new UnityEngine.Vector2(0, 0);
            tempChild.DOLocalMove(tempChild.GetComponent<DragObject>().startPosition, 0.1f, false).OnComplete(delegate ()
            {
                tempChild.GetComponent<Collider2D>().enabled = true;
                tempChild.gameObject.GetComponent<DragObject>().RevertSortingLayerToDefault();
            });
        }

        else
        {
            Transform tempParent1 = transform.parent;
            Transform tempParent2 = temp.transform.parent.transform;


            transform.parent = tempParent2;
            temp.transform.parent = tempParent1;

            startPosition = new UnityEngine.Vector2(0, 0);
            transform.DOLocalMove(startPosition, 0.1f, false).OnComplete(delegate ()
            {
                transform.GetComponent<Collider2D>().enabled = true;
                RevertSortingLayerToDefault();
            });

            temp.transform.GetComponent<Collider2D>().enabled = false;
            temp.transform.GetComponent<DragObject>().startPosition = new UnityEngine.Vector2(0, 0);
            temp.transform.DOLocalMove(temp.transform.GetComponent<DragObject>().startPosition, 0.1f, false).OnComplete(delegate ()
            {
                temp.transform.GetComponent<Collider2D>().enabled = true;
                temp.transform.gameObject.GetComponent<DragObject>().RevertSortingLayerToDefault();
            });
        }
    }

    private void BackToOriginalPosition()
    {
        //transform.DOScale(startScale, 0.3f);
        transform.DOLocalMove(startPosition, 0.1f, false).OnComplete(delegate ()
        {
            transform.GetComponent<Collider2D>().enabled = true;
            RevertSortingLayerToDefault();
        });
    }

    private void ChangeSortingLayer()
    {
        SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (SpriteRenderer s in sp)        
            s.sortingLayerName = "Drag";

        Canvas[] canvas = GetComponentsInChildren<Canvas>(true);
        foreach (Canvas s in canvas)
            s.sortingLayerName = "Drag";

    }

    public void RevertSortingLayerToDefault()
    {
        SpriteRenderer[] sp = GetComponentsInChildren<SpriteRenderer>(true);
        foreach (SpriteRenderer s in sp)        
            s.sortingLayerName = "Default";

        Canvas[] canvas = GetComponentsInChildren<Canvas>(true);
        foreach (Canvas s in canvas)
            s.sortingLayerName = "Default";


    }

    private void Update()
    {
        if (shootRay)
        {
            raycast = Physics2D.Raycast(transform.position, UnityEngine.Vector3.forward, Mathf.Infinity);
            if (raycast.collider != null)
            {
                if (raycast.collider == gameObject)
                    temp = null;
                else
                    temp = raycast.collider.gameObject;
            }
            else
                temp = null;
        }
    }
}


