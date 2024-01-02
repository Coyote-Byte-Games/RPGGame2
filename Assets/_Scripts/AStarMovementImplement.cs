using UnityEngine;
using Pathfinding;

public class AStarMovementImplement : MonoBehaviour, IEnemyMovementImplement
{

    public Seeker seeker;
    private Path path;
    private Vector2 direction = Vector2.zero;
    Transform target;
    Rigidbody2D rb;
    int currentWaypoint = 0;
    private bool reachedEndOfPath;

    public Vector2 GetPracticalDirection()
    {
        return direction;
    }
    #region Lifecycle methods
    public void Start()
    {
        //This makes it slight pre-emptive. iN THEORY, this will be enough of a nudge, unless you're on an OG macintosh
        float repeatTime = CombatManager.instance.ClockDuration - 0.01f;

        //This way we only use it when we abosultely must
        InvokeRepeating(nameof(CalculateDirection), 0, repeatTime);
    }
    #endregion
    private void CalculateDirection()
    {
        if (seeker.IsDone())
        {

            seeker.StartPath(rb.position, target.position, onPathComplete);

        }
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
        direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        currentWaypoint++;
    }

    private void onPathComplete(Path p)
    {
        if (path is not null)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

}