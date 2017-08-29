using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameControl : MonoBehaviour {

    //Game states
    public enum GameState
    {
        playing,
        gameOver,
        restarting
    }

    public static GameControl instance;
    private GameState gameControlState;

    //-------API methods------------
    //Singleton creation
    private void Awake()
    {
        if (instance== null)
        {
            instance = this;
        }
        else if(instance!= this)
        {
            Destroy(this.gameObject);
        }
    }

    public void Start()
    {
        gameControlState = GameState.playing;
    }

    //-------custom methods------------
    public void Restart()
    {
        gameControlState = GameState.restarting;
        StartCoroutine(RestartTransition());
    }

    private IEnumerator RestartTransition()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync((int)GameState.playing);
    }

    #region Properties
    public GameState GameControlState
    {
        get
        {
            return gameControlState;
        }

        set
        {
            gameControlState = value;
        }
    }
    #endregion
}
