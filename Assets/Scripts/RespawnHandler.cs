using System.Collections;
using UnityEngine;

public class RespawnHandler : MonoBehaviour
{
    [SerializeField]
    float maxRespawnTime;


    InteractableBananaHandler bananaHandler;


    void Awake()
    {
        bananaHandler = GetComponent<InteractableBananaHandler>();
    }

    void OnEnable()
    {
        bananaHandler.OnInvisible += _OnInvisible;
    }

    void OnDestroy()
    {
        bananaHandler.OnInvisible -= _OnInvisible;
    }

    void _OnInvisible()
    {
        StartCoroutine(_Respawn_Callback(maxRespawnTime));
    }

    IEnumerator _Respawn_Callback(float time)
    {
        yield return new WaitForSeconds(time);
        bananaHandler.Show(true);
    }
}
