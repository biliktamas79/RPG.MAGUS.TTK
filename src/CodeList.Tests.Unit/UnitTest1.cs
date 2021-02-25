using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeList.Tests.Unit
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TreeNodeStringString_CanSerialize_ThenDeserialize_Json()
        {
            var rootNodes = new List<TreeNode<string, string>>()
            {
                new TreeNode<string, string>() { Key = "key1", Value = null },
                new TreeNode<string, string>() { Key = "key2", Value = string.Empty },
                new TreeNode<string, string>() { Key = "key3", Value = "value3" },
                new TreeNode<string, string>() { Key = "key4", Value = "value4", Children = new List<TreeNode<string, string>>()
                {
                    new TreeNode<string, string>() { Key = "key4subkey1", Value = null },
                    new TreeNode<string, string>() { Key = "key4subkey2", Value = string.Empty, Children = new List<TreeNode<string, string>>()
                    {
                        new TreeNode<string, string>() { Key = "key4subkey2subkey1", Value = null },
                        new TreeNode<string, string>() { Key = "key4subkey2subkey2", Value = string.Empty },
                        new TreeNode<string, string>() { Key = "key4subkey2subkey3", Value = "subsubvalue3", Children = new List<TreeNode<string, string>>()
                        {
                            new TreeNode<string, string>() { Key = "key4subkey2subkey3subkey1", Value = null },
                            new TreeNode<string, string>() { Key = "key4subkey2subkey3subkey2", Value = string.Empty },
                            new TreeNode<string, string>() { Key = "key4subkey2subkey3subkey3", Value = "subsubsubvalue3" },
                            new TreeNode<string, string>() { Key = "key4subkey2subkey3subkey4", Value = "subsubsubvalue4" },
                        }},
                        new TreeNode<string, string>() { Key = "key4subkey2subkey4", Value = "subsubvalue4" },
                    }},
                    new TreeNode<string, string>() { Key = "key4subkey3", Value = "subvalue3" },
                    new TreeNode<string, string>() { Key = "key4subkey4", Value = "subvalue4", Children = new List<TreeNode<string, string>>()
                    {
                        new TreeNode<string, string>() { Key = "key4subkey4subkey1", Value = null },
                        new TreeNode<string, string>() { Key = "key4subkey4subkey2", Value = string.Empty },
                        new TreeNode<string, string>() { Key = "key4subkey4subkey3", Value = "subsubvalue3" },
                        new TreeNode<string, string>() { Key = "key4subkey4subkey4", Value = "subsubvalue4" },
                    }},
                }},
            };

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var node in rootNodes)
            {
                foreach (var n in node.EnumerateWidth(true))
                //foreach (var n in node.EnumerateDepth(true))
                {
                    sb.AppendLine(n.ToKeyPathString());
                }
            }
            var sBefore = sb.ToString();

            var jsonString = Utils.SerializeToJsonString(rootNodes);

            var deserialized = Utils.DeserializeFromJsonString<List<TreeNode<string, string>>>(jsonString);

            sb.Clear();
            foreach (var node in deserialized)
            {
                foreach (var n in node.EnumerateWidth(true))
                //foreach (var n in node.EnumerateDepth(true))
                {
                    sb.AppendLine(n.ToKeyPathString());
                }
            }
            var sAfter = sb.ToString();

            Assert.AreEqual(sBefore, sAfter);
        }
    }
}
