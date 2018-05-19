using System.Collections;
using UnityEngine;
using Pathfinding;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float updateRate = 2f;

    private Seeker _seeker;
    private Rigidbody2D _rb;

    public Path Path;

    public float Speed = 300f;
    public ForceMode2D FMode;

    [HideInInspector] public bool PathIsEnded = false;

    public float NextWaypointDistance = 3;
    private int _currentWaypoint = 0;
    private bool _searchingForPlayer;

    void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();

        if (!PlayerFound())
            return;

        _seeker.StartPath(transform.position, target.position, OnPathComplete);

        StartCoroutine(UpdatePath());
    }

    private bool PlayerFound()
    {
        if (target != null) return true;
        if (_searchingForPlayer) return false;
        _searchingForPlayer = true;
        StartCoroutine(SearchForPlayer());
        return false;
    }

    void FixedUpdate()
    {
        if (!PlayerFound())
            return;

        if (Path == null)
            return;

        if (_currentWaypoint >= Path.vectorPath.Count)
        {
            if (PathIsEnded)
                return;

            Debug.Log("End of path reached");
            PathIsEnded = true;
        }

        PathIsEnded = false;

        if (_currentWaypoint >= Path.vectorPath.Count)
            return;

        Vector3 dir = (Path.vectorPath[_currentWaypoint] - transform.position).normalized;
        dir *= Speed * Time.fixedDeltaTime;
        _rb.AddForce(dir, FMode);


        float dist = Vector3.Distance(transform.position, Path.vectorPath[_currentWaypoint]);
        if (dist < NextWaypointDistance)
        {
            _currentWaypoint++;
            return;
        }
    }

    private IEnumerator SearchForPlayer()
    {
        GameObject sResult = GameObject.FindGameObjectWithTag("Player");
        if (sResult == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchForPlayer());
        }
        else
        {
            target = sResult.transform;
            _searchingForPlayer = false;
            StartCoroutine(UpdatePath());
           yield return false;
        }
    }

    private IEnumerator UpdatePath()
    {
        if (!PlayerFound())
            yield return false;

        _seeker.StartPath(transform.position, target.position, OnPathComplete);

        yield return new WaitForSeconds(1f / updateRate);
        StartCoroutine(UpdatePath());
    }

    private void OnPathComplete(Path p)
    {
        Debug.Log($"We got a path. Error?: {p.error}");
        if (!p.error)
        {
            Path = p;
            _currentWaypoint = 0;
        }
    }

}
