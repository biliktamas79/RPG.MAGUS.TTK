using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CodeList
{
    /// <summary>
    /// Generic treenode containing a key and a value.
    /// </summary>
    /// <typeparam name="Tkey">The type of the node key.</typeparam>
    /// <typeparam name="Tvalue">The type of the node value.</typeparam>
    public class TreeNode<Tkey, Tvalue>
    {
        public Tkey Key { get; set; }

        public Tkey Value { get; set; }

        [JsonIgnore]
        public TreeNode<Tkey, Tvalue> Parent { get; set; }
        
        public IList<TreeNode<Tkey, Tvalue>> Children { get; set; }

        public override string ToString()
        {
            return ToKeyPathString();
        }

        public string ToKeyPathString(string separator = ".")
        {
            return (this.Parent == null)
                ? this.Key?.ToString() ?? string.Empty
                : AppendPathStringTo(new StringBuilder(), node => node.Key, separator).ToString();
        }

        public string ToValuePathString(string separator = ".")
        {
            return (this.Parent == null)
                ? this.Value?.ToString() ?? string.Empty
                : AppendPathStringTo(new StringBuilder(), node => node.Value, separator).ToString();
        }

        public string ToPathString<T>(Func<TreeNode<Tkey, Tvalue>, T> getNodePathComponent, string separator = ".")
        {
            if (getNodePathComponent == null)
                throw new ArgumentNullException(nameof(getNodePathComponent));

            return (this.Parent == null)
                ? getNodePathComponent(this)?.ToString() ?? string.Empty
                : AppendPathStringTo(new StringBuilder(), getNodePathComponent, separator).ToString();
        }

        protected StringBuilder AppendPathStringTo<T>(StringBuilder sb, Func<TreeNode<Tkey, Tvalue>, T> getNodePathComponent, string separator = ".")
        {
            // not needed unless method gets public
            //if (getNodePathComponent == null)
            //    throw new ArgumentNullException(nameof(getNodePathComponent));

            if (this.Parent == null)
            {
                sb.Append(getNodePathComponent(this));
            }
            else
            {
                this.Parent.AppendPathStringTo(sb, getNodePathComponent)
                    .Append(separator)
                    .Append(getNodePathComponent(this));
            }
            return sb;
        }

        /// <summary>
        /// Enumerating through the whole tree by enumerating siblings before children.
        /// </summary>
        /// <param name="includeSelf">True to include self, otherwise false.</param>
        /// <returns></returns>
        public IEnumerable<TreeNode<Tkey, Tvalue>> EnumerateWidth(bool includeSelf = true)
        {
            if (includeSelf)
                yield return this;

            // if there are children
            if ((this.Children != null) && (this.Children.Count > 0))
            {
                // enumerating direct children
                foreach (var child in this.Children)
                {
                    yield return child;
                }

                // enumerating children of direct children
                foreach (var child in this.Children)
                {
                    foreach (var grandChild in child.EnumerateWidth(false))
                    {
                        yield return grandChild;
                    }
                }
            }
        }

        /// <summary>
        /// Enumerating through the whole tree by enumerating children before siblings.
        /// </summary>
        /// <param name="includeSelf">True to include self, otherwise false.</param>
        /// <returns></returns>
        public IEnumerable<TreeNode<Tkey, Tvalue>> EnumerateDepth(bool includeSelf = true)
        {
            if (includeSelf)
                yield return this;

            // if there are children
            if ((this.Children != null) && (this.Children.Count > 0))
            {
                // enumerating direct children
                foreach (var child in this.Children)
                {
                    // enumerate grandchildren, including self
                    foreach (var grandChild in child.EnumerateDepth(true))
                    {
                        yield return grandChild;
                    }
                }
            }
        }
    }
}
