// Copyright (c) Ethereal. All rights reserved.

using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// ITreeNode
    /// </summary>
    public class TreeNode<TSource>
    {
        /// <summary>
        /// Identifies whether the node is an end node
        /// </summary>
        public bool IsLeaf => !Children.Any();

        /// <summary>
        /// Children nodes
        /// </summary>
        public IList<TSource> Children { get; private set; } = new List<TSource>();
    }
}
