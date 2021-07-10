using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance;

    [SerializeField]
    private GameObject poolingObjectprefabGold;
    [SerializeField]
    private GameObject poolingObjectprefabExp;
    [SerializeField]
    private int Count = 10;

    private Queue<Gold> poolingObjectQueueGold = new Queue<Gold>();
    private Queue<Exp> poolingObjectQueueExp = new Queue<Exp>();

    private void Awake()
    {
        Instance = this;
        Initialize(Count);
    }

    private Gold CreateNewObjectGold()
    {
        var newObj = Instantiate(poolingObjectprefabGold, transform).GetComponent<Gold>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    private void Initialize(int count)
    {
        for (int i = 0; count > i; i++)
        {
            poolingObjectQueueGold.Enqueue(CreateNewObjectGold());
        }

        for (int i = 0; count > i; i++)
        {
            poolingObjectQueueExp.Enqueue(CreateNewObjectExp());
        }
    }

    public static Gold getObjectGold()
    {
        if (Instance.poolingObjectQueueGold.Count > 0)
        {
            var obj = Instance.poolingObjectQueueGold.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObjectGold();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true); 
            return newObj;
        }
    }

    private Exp CreateNewObjectExp()
    {
        var newObj = Instantiate(poolingObjectprefabExp, transform).GetComponent<Exp>();
        newObj.gameObject.SetActive(false);
        return newObj;
    }

    public static void ReturnObjectGold(Gold gold)
    {
        gold.gameObject.SetActive(false);
        gold.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueueGold.Equals(gold);
    }

    public static Exp getObjectExp()
    {
        if (Instance.poolingObjectQueueExp.Count > 0)
        {
            var obj = Instance.poolingObjectQueueExp.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObjectExp();
            newObj.transform.SetParent(null);
            newObj.gameObject.SetActive(true);
            return newObj;
        }
    }

    public static void ReturnObjectExp(Exp exp)
    {
        exp.gameObject.SetActive(false);
        exp.transform.SetParent(Instance.transform);
        Instance.poolingObjectQueueGold.Equals(exp);
    }

}
