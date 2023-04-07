using System.Collections.Generic;
using UnityEngine;

public class ColumnManager : MonoBehaviour
{
    public GameObject Prefab;

    // 运动速度
    public float Velocity;
    public float Duration;

    // 两个队列维护运动中和静止状态的游戏对象
    private Queue<GameObject> _inactive_queue;
    private Queue<GameObject> _active_queue;

    private void Awake()
    {
        _active_queue = new Queue<GameObject>(8);
        _inactive_queue = new Queue<GameObject>(8);
    }

    // Start is called before the first frame update
    void Start()
    {
        // 定时激活
        InvokeRepeating("ActivateColumn", Duration, Duration);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.Instance.IsOver)
        {
            PauseColumn();
        }
        else if (GameController.Instance.IsPlay)
        {
            DeactivateColumn();
        }
    }

    private void PauseColumn()
    {
        foreach (GameObject item in _active_queue)
        {
            if (item != null)
            {
                item.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
        }
    }

    private Vector3 RandomStartPosition()
    {
        return Camera.main.ViewportToWorldPoint(new Vector3(1.1f, Random.Range(0.4f, 0.6f), transform.position.z - Camera.main.transform.position.z));
    }

    private void ActivateColumn()
    {
        if (GameController.Instance.IsPlay)
        {
            GameObject obj;
            if (!_inactive_queue.TryDequeue(out obj))
            {
                obj = Instantiate(Prefab, RandomStartPosition(), Quaternion.identity, transform);
            }
            obj.GetComponent<Rigidbody2D>().velocity = Vector2.left * Velocity;
            _active_queue.Enqueue(obj);
        }
    }

    private void DeactivateColumn()
    {
        GameObject obj;
        if (_active_queue.TryPeek(out obj))
        {
            if (Camera.main.WorldToViewportPoint(obj.transform.position).x <= -0.1f)
            {
                obj = _active_queue.Dequeue();
                obj.transform.position = RandomStartPosition();
                obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                _inactive_queue.Enqueue(obj);
            }
        }
    }
}
