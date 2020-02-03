namespace Bizland.Core.Internals
{
    public interface IAnimationCallback
    {
        void OnFinished();

        void OnCanceled();
    }
}