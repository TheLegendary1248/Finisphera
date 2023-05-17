using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// This flag is set true during a level being cleared
    /// </summary>
    public bool onlevelClearStage { get; private set; }

    /// <summary>
    /// Is the game paused?
    /// </summary>
    public static bool isPaused { get; private set; } = false;
    /// <summary>
    /// Called when entering or exiting the paused state, passing a true on enter and vice versa
    /// </summary>
    public static event System.Action<bool> onPause;

    /// <summary>
    /// Is the game currently at the swing state(time is stopped; waiting for user input)
    /// </summary>

    public static bool isOnSwing { get; private set; } = true;
    /// <summary>
    /// Called when entering or exiting the swing state, passing a true on enter and vice versa
    /// </summary>
    public static event System.Action<bool> onSwing;
    
    public static event System.Action onNewLevel;
    public static event System.Action reachedGoal;
    //im sorry, are you complaining about this for some reason? gtfo
    public static void WinCondition() => reachedGoal?.Invoke();
    /// <summary>
    /// Time since a course was started
    /// </summary>
    public static float timeSinceCourseStart => Time.time - _timeOfCourseStart;
    public static float timeOfCourseStart => _timeOfCourseStart;
    static float _timeOfCourseStart = 0f;
    /// <summary>
    /// Time since the level was started
    /// </summary>
    public static float timeSinceLevelStart => Time.time - _timeOfLevelStart;
    public static float timeOfLevelStart => _timeOfLevelStart;
    static float _timeOfLevelStart = 0;
    
    /// <summary>
    /// Function for loading a course
    /// </summary>
    static void LoadCourse()
    {
        //Finalize this method
        InitializeSession();
    }
    /// <summary>
    /// Starts the game and initializes values
    /// </summary>
    static void InitializeSession()
    {
        //Debug.Log("Init");
        ExitPause(); 
        EnterSwing();   
        _timeOfCourseStart = _timeOfLevelStart = Time.time;
        //Notify
        onNewLevel?.Invoke();
    }
    /// <summary>
    /// Function for entering
    /// </summary>
    static void EnterPause()
    {
        if(!isOnSwing)
        {
            Time.timeScale = 0f;
        }
        isPaused = true;
        onPause?.Invoke(true);
    }
    static void ExitPause()
    {
        if(!isOnSwing)
        {
            Time.timeScale = 1f;
        }
        isPaused = false;
        onPause?.Invoke(false);
    }
    static void EnterSwing()
    {
        bouncesLeft = 3;
        Time.timeScale = 0f;
        isOnSwing = true;
        onSwing?.Invoke(true);
    }
    static void ExitSwing()
    {
        Time.timeScale = 1f;
        isOnSwing = false;
        onSwing?.Invoke(false);
    }
    /// <summary>
    /// Actually consider it the start of the game on first shot. NOT IMPLEMENTED
    /// </summary>
    void MarkStart()
    {
        throw new System.Exception();
    }
    [MenuItem("Assets/CheckObjects")]
    static void ClearCurrent()
    {
        Scene current = SceneManager.GetActiveScene();
        GameObject[] gos = current.GetRootGameObjects();
        for (int i = 0; i < gos.Length; i++)
        {
            Debug.Log(gos[i]);
            Destroy(gos[i]);
        }
        
    }
    void LoadNextLevel()
    {
        throw new System.NotImplementedException();
    }
    private void Start()
    {
#if UNITY_EDITOR
        //If the scene has gameobjects present already, treat is as an ongoing game
        if(SceneManager.GetActiveScene().GetRootGameObjects().Length > 0)
        {
            InitializeSession();
        }
#endif
        return;
    }
    private void FixedUpdate()
    {
        //Check if time scale is appropiate to current TimeScale
        if (isPaused & Time.timeScale != 0)
        {
            Debug.LogError("The game is paused, but TimeScale is NOT zero");
            Debug.Log($"p {isPaused} : s {isOnSwing} : {Time.timeScale}");
            Time.timeScale = 0f;
        }
        else
        {
            if(isOnSwing ? Time.timeScale != 0 : Time.timeScale == 0)
            {
                Debug.LogError($"The game is {(!isOnSwing ? "NOT" : "")}in the swing state, but TimeScale is {(!isOnSwing ? "" : "NOT")} zero");
                Debug.Log($"p {isPaused} : s {isOnSwing} : {Time.timeScale}");
                Time.timeScale = isOnSwing ? 0f : 1f;
            }
        }
    }
    static uint _bouncesLeft = 3;
    public static event System.Action onBouncesChanged;
    public static uint bouncesLeft {
        get => _bouncesLeft;
        set {
            if (_bouncesLeft == value) return;
            _bouncesLeft = value;
            onBouncesChanged?.Invoke();
            if (value == 0) EnterSwing();
        }
    }
    public static Dictionary<string, System.Action> Inputs = new Dictionary<string, System.Action>
    {
        {"Submit", OnSubmit },
        {"Reset", OnReset },
    };
    public static void OnSubmit()
    {
        if (isPaused) return;
        if(isOnSwing)
        {
            BallScript ball = BallScript.self;
            Vector2 force = CameraControl.MousePos() - (Vector2)ball.transform.position;
            ball.rb.AddForce(force * ball.rb.mass, ForceMode2D.Impulse);
            ExitSwing();
        }
    }
    public static void OnReset()
    {
        if (isPaused) return;
        if(!isOnSwing)
        {
            BallScript.self.ResetToLastPoint();
            EnterSwing();
        }
    }
    Vector2 CalculateForce(Rigidbody2D rb)
    {
        //Get mouse pos
        //Normalize etc, etc
        throw new System.NotImplementedException();
    }
    public static void OnBack()
    {

    }
}
