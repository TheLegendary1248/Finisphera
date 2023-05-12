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
    /// Called with boolean indicating current pause state
    /// </summary>
    public static event System.Action<bool> onPause;
    /// <summary>
    /// Is the game paused?
    /// </summary>
    public bool isPaused { get; private set; }
    /// <summary>
    /// FILL IN THIS LATER I NEED TO JUST FINISH THE GAME
    /// </summary>
    public static event System.Action<bool> onStroke;
    
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
    public static uint bouncesLeft = 3;
    //Event for session start
    //Event for level start
    //
    // Start is called before the first frame update
    void LoadCourse()
    {
        //Initialize values
        onNewLevel?.Invoke();
        _timeOfCourseStart = _timeOfLevelStart = Time.time;
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

    }
    public BounceCounterAnimator animator;
    private void Start()
    {
        return;
        animator.UpdateCurrentState();
    }
    void Update()
    {
        return;
        bool gotInput = false;
        if (Input.GetKeyDown(KeyCode.T))
        {
            gotInput = true;
            bouncesLeft += 1;
            
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            gotInput = true;
            bouncesLeft -= 1;
            
        }
        if (gotInput)
        {
            animator.UpdateCurrentState();
            Debug.Log($"Bounces Left: {bouncesLeft}");
        }
    }
}
