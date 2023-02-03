using JEngine.Core;
using UnityEngine;
using DG.Tweening;
public class MyTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMove(Vector3.one,1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
