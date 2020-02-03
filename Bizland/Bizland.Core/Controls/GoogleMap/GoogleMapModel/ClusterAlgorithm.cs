namespace Bizland.Core
{
    /// <summary>
    /// Describe the available cluster algorithms.
    /// </summary>
    public enum ClusterAlgorithm
    {
        NonHierarchicalDistanceBased,
        GridBased,

        /// <summary>
        /// Android only
        /// </summary>
        VisibleNonHierarchicalDistanceBased
    }
}