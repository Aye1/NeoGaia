using UnityEngine;

public class GameObjectProvider : MonoBehaviour {

    public DialogBox dialogBox;

    public static GameObjectProvider instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
