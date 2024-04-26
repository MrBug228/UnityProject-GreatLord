using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ObjectPool : MonoBehaviour

{
    public List<GameObject> pooledObjectList;

    [SerializeField]
    private GameObject prefabObject;

    [SerializeField]
    private int poolSize = 10;

    [SerializeField]
    private Transform objectHolder;


    private void Start()
    {
        this.pooledObjectList = new List<GameObject>();
        GameObject newObject;

        for (int i = 0; i < this.poolSize; i++)
        {
            newObject = Instantiate(prefabObject);

            this.AddObjectToPool(newObject);
        }
    }

    public GameObject GetObjectFromPool()
    {
        /* for (int i = 0; i < this.pooledObjectList.poolSize; i++) // chu y poolsze ko tu cap nhap sau khi add them phan tu moi vao, nen duyet qua Count  this.pooledObjectList.Count;
         {
             if (!this.pooledObjectList[i].activeInHierarchy)
             {
                 return this.pooledObjectList[i];
             }
         }*/
        foreach (GameObject obj in this.pooledObjectList)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        GameObject newObject = Instantiate(this.prefabObject);

        this.AddObjectToPool(newObject);

        return newObject;
    }
    private void  AddObjectToPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(this.objectHolder);
        pooledObjectList.Add(obj);
    }
    public void Release(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }


}


