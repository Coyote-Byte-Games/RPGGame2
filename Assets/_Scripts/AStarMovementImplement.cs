using UnityEngine;
using Pathfinding;
using Sirenix.OdinInspector;
using UnityEngine.UIElements;
using Sirenix.Serialization;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

public class AStarMovementImplement : SerializedMonoBehaviour, IEnemyMovementImplement
{
    //! Assume that you need to use Serialized MB to use interfaces and polymorphism

    public Transform target;
    public Seeker seeker;
    [OdinSerialize] public Path path;
    public Vector2Int direction = Vector2Int.down;
    [SerializeField] public Rigidbody2D rb;
    int currentWaypoint = 0;
    public bool reachedEndOfPath;
    public float waypointDistance = 0.5f;

    public Vector2Int GetPracticalDirection()
    {
        return direction;

    }
    #region Lifecycle methods

    public void Start()
    {
        //This makes it slight pre-emptive. iN THEORY, this will be enough of a nudge, unless you're on an OG macintosh
        float repeatTime = CombatManager.instance.ClockDuration - 0.01f;

        //This way we only use it when we abosultely must
        InvokeRepeating(nameof(StartPath), 0, repeatTime);
    }
    private void StartPath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, onPathComplete);
        }
    }
    public void FixedUpdate()
    {

        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        var tempDirection = (((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized);
        //i don't like it but whatevs
        direction.x = Mathf.RoundToInt(tempDirection.x);
        direction.y = Mathf.RoundToInt(tempDirection.y);
        Debug.Log("ah ah ah " + path.vectorPath[currentWaypoint]);

        if (Vector2.Distance(path.vectorPath[currentWaypoint], rb.position) <= waypointDistance)
        {
            currentWaypoint++;
        }

    }
    #endregion

    private void onPathComplete(Path p)
    {
        if (!path.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

}