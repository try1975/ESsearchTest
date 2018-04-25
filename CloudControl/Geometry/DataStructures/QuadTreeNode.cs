#region

using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

#endregion

namespace Gma.CodeCloud.Controls.Geometry.DataStructures
{
    public class QuadTreeNode<T> where T : LayoutItem
    {
        private RectangleF _mBounds;

        private QuadTreeNode<T>[] _mNodes = new QuadTreeNode<T>[0];

        public QuadTreeNode(RectangleF bounds)
        {
            _mBounds = bounds;
        }

        public bool IsEmpty => _mBounds.IsEmpty || _mNodes.Length == 0;

        public RectangleF Bounds => _mBounds;

        public int Count
        {
            get
            {
                var count = 0;

                foreach (var node in _mNodes)
                    count += node.Count;

                count += Contents.Count;

                return count;
            }
        }

        public IEnumerable<T> SubTreeContents
        {
            get
            {
                IEnumerable<T> results = new T[0];

                foreach (var node in _mNodes)
                    results = results.Concat(node.SubTreeContents);

                results = results.Concat(Contents);
                return results;
            }
        }

        public Stack<T> Contents { get; } = new Stack<T>();


        public bool HasContent(RectangleF queryArea)
        {
            var queryResult = Query(queryArea);
            return IsEmptyEnumerable(queryResult);
        }

        private static bool IsEmptyEnumerable(IEnumerable<T> queryResult)
        {
            using (var enumerator = queryResult.GetEnumerator())
            {
                return enumerator.MoveNext();
            }
        }

        /// <summary>
        ///     Query the QuadTree for items that are in the given area
        /// </summary>
        /// <param name="queryArea">
        ///     <returns></returns>
        /// </param>
        public IEnumerable<T> Query(RectangleF queryArea)
        {
            // this quad contains items that are not entirely contained by
            // it's four sub-quads. Iterate through the items in this quad 
            // to see if they intersect.
            foreach (var item in Contents)
                if (queryArea.IntersectsWith(item.Rectangle))
                    yield return item;

            foreach (var node in _mNodes)
            {
                if (node.IsEmpty)
                    continue;

                // Case 1: search area completely contained by sub-quad
                // if a node completely contains the query area, go down that branch
                // and skip the remaining nodes (break this loop)
                if (node.Bounds.Contains(queryArea))
                {
                    var subResults = node.Query(queryArea);
                    foreach (var subResult in subResults)
                        yield return subResult;
                    break;
                }

                // Case 2: Sub-quad completely contained by search area 
                // if the query area completely contains a sub-quad,
                // just add all the contents of that quad and it's children 
                // to the result set. You need to continue the loop to test 
                // the other quads
                if (queryArea.Contains(node.Bounds))
                {
                    var subResults = node.SubTreeContents;
                    foreach (var subResult in subResults)
                        yield return subResult;
                    continue;
                }

                // Case 3: search area intersects with sub-quad
                // traverse into this quad, continue the loop to search other
                // quads
                if (node.Bounds.IntersectsWith(queryArea))
                {
                    var subResults = node.Query(queryArea);
                    foreach (var subResult in subResults)
                        yield return subResult;
                }
            }
        }


        public void Insert(T item)
        {
            // if the item is not contained in this quad, there's a problem
            if (!_mBounds.Contains(item.Rectangle))
            {
                Trace.TraceWarning("feature is out of the bounds of this quadtree node");
                return;
            }

            // if the subnodes are null create them. may not be sucessfull: see below
            // we may be at the smallest allowed size in which case the subnodes will not be created
            if (_mNodes.Length == 0)
                CreateSubNodes();

            // for each subnode:
            // if the node contains the item, add the item to that node and return
            // this recurses into the node that is just large enough to fit this item
            foreach (var node in _mNodes)
                if (node.Bounds.Contains(item.Rectangle))
                {
                    node.Insert(item);
                    return;
                }

            // if we make it to here, either
            // 1) none of the subnodes completely contained the item. or
            // 2) we're at the smallest subnode size allowed 
            // add the item to this node's contents.
            Contents.Push(item);
        }

        public void ForEach(QuadTree<T>.QuadTreeAction action)
        {
            action(this);

            // draw the child quads
            foreach (var node in _mNodes)
                node.ForEach(action);
        }

        private void CreateSubNodes()
        {
            // the smallest subnode has an area 
            if (_mBounds.Height * _mBounds.Width <= 10)
                return;

            var halfWidth = _mBounds.Width / 2f;
            var halfHeight = _mBounds.Height / 2f;

            _mNodes = new QuadTreeNode<T>[4];
            _mNodes[0] = new QuadTreeNode<T>(new RectangleF(_mBounds.Location, new SizeF(halfWidth, halfHeight)));
            _mNodes[1] = new QuadTreeNode<T>(new RectangleF(new PointF(_mBounds.Left, _mBounds.Top + halfHeight),
                new SizeF(halfWidth, halfHeight)));
            _mNodes[2] = new QuadTreeNode<T>(new RectangleF(new PointF(_mBounds.Left + halfWidth, _mBounds.Top),
                new SizeF(halfWidth, halfHeight)));
            _mNodes[3] =
                new QuadTreeNode<T>(new RectangleF(new PointF(_mBounds.Left + halfWidth, _mBounds.Top + halfHeight),
                    new SizeF(halfWidth, halfHeight)));
        }
    }
}