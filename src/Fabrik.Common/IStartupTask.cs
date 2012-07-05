
namespace Fabrik.Common
{   
    /// <summary>
    /// An interface for tasks that should be executed on startup.
    /// </summary>
    public interface IStartupTask
    {
        void Execute();
    }
}
