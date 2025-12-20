using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    public float elapsedTime {get; private set;}

    void Awake(){
        Instance = this;
    }
    void Update(){
        elapsedTime += Time.deltaTime;
    }
}
