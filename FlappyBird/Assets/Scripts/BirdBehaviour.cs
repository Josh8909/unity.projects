using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBehaviour : MonoBehaviour
{
    private bool _isAlive;
    private Animator _animator;
    private Rigidbody2D _body;

    public AudioSource WingAudio;
    public AudioSource HitAudio;
    public AudioSource DieAudio;

    private Vector2 _force;
    // Start is called before the first frame update
    void Start()
    {
        TransformBird();
        Time.timeScale = 0;

        _animator = transform.GetComponent<Animator>();
        _body = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAlive && GameController.IsTaped())
        {
            if (GameController.Instance.IsReady)
            {
                GameController.Instance.StartGame();
                Time.timeScale = 1;
            }
            else if (GameController.Instance.IsPlay)
            {
                _animator.SetTrigger("Fly");
                // 施加瞬时力量
                _body.AddForce(_force * 5, ForceMode2D.Impulse);
                WingAudio.Play();
            }
        }
    }

    void TransformBird()
    {
        _isAlive = true;
        transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0.2f, 0.5f, transform.position.z - Camera.main.transform.position.z));

        // 动态计算力的大小
        _force = Camera.main.ViewportToWorldPoint(new Vector3(0.2f, 0.6f, transform.position.z - Camera.main.transform.position.z)) - transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            HitAudio.Play();
            if (_isAlive)
            {
                DieAudio.Play();
                _isAlive = false;
                GameController.Instance.GameOver();
            }
        }
    }
}
