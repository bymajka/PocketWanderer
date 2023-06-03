using Pathfinding;

namespace EnemySystem
{
    public interface IPathFindingListener
    {
        void OnPathCompleted(Path path);
    }
}