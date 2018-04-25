#region

using System.Collections.Generic;
using System.Drawing;

#endregion

namespace Gma.CodeCloud.Controls.Geometry.DataStructures
{
    /// <summary>
    ///     A Quadtree is a structure designed to partition space so
    ///     that it's faster to find out what is inside or outside a given
    ///     area. See http://en.wikipedia.org/wiki/Quadtree
    ///     This QuadTree contains items that have an area (RectangleF)
    ///     it will store a reference to the item in the quad
    ///     that is just big enough to hold it. Each quad has a bucket that
    ///     contain multiple items.
    /// </summary>
    public class QuadTree<T> where T : LayoutItem
    {
        #region Delegates

        public delegate void QuadTreeAction(QuadTreeNode<T> obj);

        #endregion

        private readonly RectangleF _mRectangle;
        private readonly QuadTreeNode<T> _mRoot;

        public QuadTree(RectangleF rectangle)
        {
            _mRectangle = rectangle;
            _mRoot = new QuadTreeNode<T>(_mRectangle);
        }

        public void ForEach(QuadTreeAction action)
        {
            _mRoot.ForEach(action);
        }

        #region IQuadTree<T> Members

        public int Count => _mRoot.Count;

        public void Insert(T item)
        {
            _mRoot.Insert(item);
        }

        public IEnumerable<T> Query(RectangleF area)
        {
            return _mRoot.Query(area);
        }

        public bool HasContent(RectangleF area)
        {
            var result = _mRoot.HasContent(area);
            return result;
        }

        #endregion
    }
}