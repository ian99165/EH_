using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public enum MonsterState
    {
        Patrolling,
        Chasing
    }

    public MonsterState currentState = MonsterState.Patrolling;
    public Transform player;
    public float detectionRadius = 5f;

    public float moveSpeed = 0.5f;
    public float chasingSpeed = 1f;
    public float minChangeDirectionInterval = 2f;
    public float maxChangeDirectionInterval = 5f;
    public float minPauseTime = 1f;
    public float maxPauseTime = 3f;

    private Vector3 targetDirection;
    private float timeToChangeDirection;
    private float timeToPause;
    private bool isPaused = false;

    private Rigidbody rb;

    // 新增：是否為敵對狀態
    public bool isHostile = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.mass = 1f;
        rb.drag = 0.5f;
        rb.angularDrag = 2f;

        SetRandomDirection();
        SetRandomChangeDirectionTime();
        SetRandomPauseTime();
    }

    void FixedUpdate()
    {
        // 若非敵對狀態，強制切回巡邏模式
        if (!isHostile && currentState != MonsterState.Patrolling)
        {
            SwitchToPatrolling();
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (isHostile && distanceToPlayer <= detectionRadius && currentState != MonsterState.Chasing)
        {
            SwitchToChasing();
        }
        else if (distanceToPlayer > detectionRadius && currentState != MonsterState.Patrolling)
        {
            SwitchToPatrolling();
        }

        switch (currentState)
        {
            case MonsterState.Patrolling:
                Patrol();
                break;

            case MonsterState.Chasing:
                ChasePlayer();
                break;
        }
    }

    void Patrol()
    {
        if (isPaused)
        {
            if (Time.time >= timeToPause)
            {
                isPaused = false;
                SetRandomDirection();
                SetRandomChangeDirectionTime();
            }
        }
        else
        {
            rb.MovePosition(transform.position + targetDirection * moveSpeed * Time.deltaTime);
            RotateTowardsMovementDirection();

            if (Time.time >= timeToChangeDirection)
            {
                isPaused = true;
                SetRandomPauseTime();
            }
        }
    }

    void ChasePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        rb.MovePosition(transform.position + directionToPlayer * chasingSpeed * Time.deltaTime);

        RotateTowardsMovementDirection(directionToPlayer);
    }

    void SetRandomDirection()
    {
        targetDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
    }

    void SetRandomChangeDirectionTime()
    {
        timeToChangeDirection = Time.time + Random.Range(minChangeDirectionInterval, maxChangeDirectionInterval);
    }

    void SetRandomPauseTime()
    {
        timeToPause = Time.time + Random.Range(minPauseTime, maxPauseTime);
    }

    void RotateTowardsMovementDirection(Vector3 targetDirection = default)
    {
        if (targetDirection == default)
        {
            targetDirection = this.targetDirection;
        }

        // 確保方向僅在水平面上
        targetDirection.y = 0;

        // 計算目標旋轉
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // 使用平滑過渡旋轉，限制僅在 Y 軸
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f * Time.deltaTime);
    }


    public void SwitchToChasing()
    {
        currentState = MonsterState.Chasing;
        moveSpeed = chasingSpeed;
    }

    public void SwitchToPatrolling()
    {
        currentState = MonsterState.Patrolling;
        moveSpeed = 0.5f;
        SetRandomDirection();
        SetRandomChangeDirectionTime();
    }

    // 新增：啟用敵對狀態的方法
    public void EnableHostile()
    {
        isHostile = true;
    }

    // 新增：禁用敵對狀態的方法
    public void DisableHostile()
    {
        isHostile = false;
        SwitchToPatrolling();
    }
}
