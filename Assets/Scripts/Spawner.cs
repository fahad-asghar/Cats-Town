using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] Image objectToSpawnImage;
    [SerializeField] List<GameObject> objectsToSpawn = new List<GameObject>();
    [SerializeField] Transform platformsParent;
    [SerializeField] Text timerText;
    [SerializeField] int timeToSpawn;
    private int tempTime;

    private int index;
    public static Spawner instance;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        tempTime = timeToSpawn;
        ObjectToSpawn();    
    }

    private void ObjectToSpawn()
    {
        if (!GameHandler.instance.balloonEnabled)
        {
            tempTime = 10;
            index = 0;
        }
        else
        {
            tempTime = 5;
            index = 1;
        }

        objectToSpawnImage.sprite = objectsToSpawn[index].GetComponent<SpriteRenderer>().sprite;
        objectToSpawnImage.SetNativeSize();
        timeToSpawn = tempTime;
        Spawn();
    }

    public void BalloonActivated()
    {
        index = 1;
        objectToSpawnImage.sprite = objectsToSpawn[index].GetComponent<SpriteRenderer>().sprite;
        objectToSpawnImage.SetNativeSize();
    }

    public void BalloonDeActivated()
    {
        index = 0;
        objectToSpawnImage.sprite = objectsToSpawn[index].GetComponent<SpriteRenderer>().sprite;
        objectToSpawnImage.SetNativeSize();
    }

    private void Spawn()
    {
        timerText.text = timeToSpawn.ToString();

        if (timeToSpawn == 0)
        {
            for (int i = 0; i < platformsParent.childCount; i++)
            {
                if (platformsParent.GetChild(i).childCount == 0 && platformsParent.GetChild(i).gameObject.activeSelf)
                {
                    GameObject obj = Instantiate(objectsToSpawn[index], new Vector3(platformsParent.GetChild(i).transform.position.x, platformsParent.GetChild(i).transform.position.y, 0), Quaternion.identity);
                    obj.name = objectsToSpawn[index].name;
                    obj.transform.parent = platformsParent.GetChild(i);
                    obj.transform.localPosition = new Vector2(0, 0);
                    obj.GetComponent<DragObject>().startPosition = obj.transform.localPosition;
                    obj.GetComponent<DragObject>().startScale = obj.transform.localScale;
                    ObjectToSpawn();
                    return;
                }
                
            }
            Invoke("Spawn", 1);
            return;

        }
        timeToSpawn--;

        Invoke("Spawn", 1);      
    }
}
