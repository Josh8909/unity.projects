using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 1. 按下空格，游戏开始
// 2. 小鸟碰到柱子，游戏结束
public class GameController : MonoBehaviour
{
    public static GameController Instance { private set; get; }

    private enum GameState { READY, PLAY, OVER }

    private GameState _state;

    public bool IsReady
    { 
        get => GameState.READY == _state;
    }

    public bool IsPlay
    {
        get => GameState.PLAY == _state;
    }

    public bool IsOver
    {
        get => GameState.OVER == _state;
    }

    public AudioSource PlayAudio;

    public int Score { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        _state = GameState.READY;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTaped() && IsOver)
        {
            SceneManager.LoadScene("GameScene");
            _state = GameState.READY;
            Score = 0;
        }
    }

    public void StartGame()
    {
        _state = GameState.PLAY;
        PlayAudio.Play();
    }

    public void GameOver()
    {
        _state = GameState.OVER;
        PlayAudio.Stop();
    }

    public static bool IsTaped()
    {
        return Input.GetKeyDown(KeyCode.Space) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began);
    }
}
