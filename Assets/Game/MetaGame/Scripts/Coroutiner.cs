using System.Collections;
using UnityEngine;


public class Coroutiner : MonoBehaviour
{

    private static Coroutiner _instance = null;
    public static Coroutiner Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject goInst = new GameObject("Coroutiner", typeof(Coroutiner));
                _instance = goInst.GetComponent<Coroutiner>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null || _instance == this)
            _instance = this;
        else
            GameObject.Destroy(this.gameObject);
    }

    public static Coroutine Start(IEnumerator routine)
    { return Coroutiner.Instance.StartCoroutine(routine); }

    public static void Stop(IEnumerator routine)
    { Coroutiner.Instance.StopCoroutine(routine); }
    public static void Stop(Coroutine routine)
    { Coroutiner.Instance.StopCoroutine(routine); }
    public static void StopAll()
    { Coroutiner.Instance.StopAllCoroutines(); }
}
