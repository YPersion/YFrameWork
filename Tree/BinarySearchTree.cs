using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 二叉搜索树
/// </summary>
/// <typeparam name="T"></typeparam>
public class BinarySearchTree<T> : ICollection<T> where T : IComparable<T>
{
    /// <summary>
    /// 根节点
    /// </summary>
    private BinaryTreeNode<T> root;
    public BinaryTreeNode<T> Root
    {
        get { return root; }
        set { root = value; }
    }
    /// <summary>
    /// 节点数量
    /// </summary>
    public int NodeCount { get; set; }

    public bool IsEmpty
    {
        get
        {
            return Root == null;
        }
    }

    public T MinValue
    {
        get
        {
            if (IsEmpty)
            {
                return default;
            }
            BinaryTreeNode<T> tempNode = root;
            while (null != tempNode.lNode)
            {
                tempNode = tempNode.lNode;
            }
            return tempNode.value;
        }
    }

    public T MaxValue
    {
        get
        {
            if (IsEmpty)
            {
                return default;
            }
            BinaryTreeNode<T> tempNode = root;
            while (null != tempNode.rNode)
            {
                tempNode = tempNode.rNode;
            }
            return tempNode.value;
        }
    }


    public int Count => NodeCount + 1;

    public bool IsReadOnly => throw new NotImplementedException();

    public BinarySearchTree()
    {
        NodeCount = 0;
    }
    public BinarySearchTree(BinaryTreeNode<T> root)
    {
        Root = root;
        NodeCount = 0;
    }


    //支持泛型
    public IEnumerator<T> GetEnumerator()
    {
        foreach (BinaryTreeNode<T> item in MidTraversal(root))
        {
            yield return item.value;
        }
    }

    //支持非泛型
    IEnumerator IEnumerable.GetEnumerator()
    {
        foreach (BinaryTreeNode<T> item in MidTraversal(root))
        {
            yield return item.value;
        }
    }

    public void Add(T item)
    {
        if (null == root)
        {
            root = new BinaryTreeNode<T>(item);
            NodeCount++;
        }
        else if (Contains(item))
        {
            return;
        }
        else
        {
            Insert(item);
        }
    }

    private void Insert(T item)
    {
        BinaryTreeNode<T> tempNode = root;
        while (true)
        {
            int comparedValue = tempNode.value.CompareTo(item);
            //参数比根大 寻找其左子叶
            if (comparedValue < 0)
            {
                if (null == tempNode.lNode)
                {
                    tempNode.lNode = new BinaryTreeNode<T>(item);
                    tempNode.lNode.parent = tempNode;
                    NodeCount++;
                }
                else
                {
                    tempNode = tempNode.lNode;
                }
            }
            else if (comparedValue > 0)
            {
                if (null == tempNode.rNode)
                {
                    tempNode.rNode = new BinaryTreeNode<T>(item);
                    tempNode.rNode.parent = tempNode;
                    NodeCount++;
                }
                else
                {
                    tempNode = tempNode.rNode;
                }
            }
            else
            {
                return;
            }
        }


    }

    public void Clear()
    {
        foreach (BinaryTreeNode<T> item in MidTraversal(root))
        {
            item.Clear();
        }
        //是否需要遍历将之前的清空掉
        root = null;
    }

    public bool Contains(T item)
    {
        if (IsEmpty) return false;

        BinaryTreeNode<T> tempNode = root;

        while (null != tempNode)
        {
            int comparedValue = tempNode.value.CompareTo(item);
            if (comparedValue == 0)
                return true;
            else if (comparedValue < 0)
                //参数比根大 则在左子叶
                tempNode = tempNode.lNode;
            else
                tempNode = tempNode.rNode;
        }
        return false;
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    //简单粗暴，移除 将之后的重新根据规则add
    public bool Remove(T _item)
    {
        BinaryTreeNode<T> targetNode = Find(_item);
        if (targetNode == null)
            return false;
        List<T> values = new List<T>();//需要重新添加的节点对象
        foreach (BinaryTreeNode<T> node in MidTraversal(targetNode.lNode))
        {
            values.Add(node.value);
        }

        foreach (BinaryTreeNode<T> node in MidTraversal(targetNode.rNode))
        {
            values.Add(node.value);
        }

        if (targetNode.lNode == targetNode)
            targetNode.lNode = null;
        else
            targetNode.rNode = null;

        targetNode.parent = null;
        foreach (var item in values)
        {
            Add(item);
        }
        return true;
    }

    //找到应该放置的节点 更改节点与父节点关系
    public bool Delete(T _item)
    {
        BinaryTreeNode<T> targetNode = Find(_item);
        if (targetNode == null)
            return false;

        //如果为根节点 清空
        if (targetNode == root)
        {
            Clear();
        }
        else if (targetNode.parent == null)
        {
            return false;
        }
        
        bool isLeftChild = null != targetNode.parent && targetNode.parent.lNode == targetNode;
        //1.当前节点无子节点
        if (null == targetNode.lNode && root == targetNode)
        {
            if (isLeftChild)
            {
                targetNode.lNode = null;
            }
            else
            {
                targetNode.rNode = null;
            }
        }
        //2.删除节点只有一个节点，且右子节点为空
        else if (null == targetNode.rNode)
        {
            if (isLeftChild)
            {
                targetNode.lNode.parent = targetNode;
                targetNode.lNode = targetNode.lNode;
            }
            else
            {
                targetNode.rNode.parent = targetNode;
                targetNode.rNode = targetNode.lNode;
            }
        }
        //3.删除节点只有一个节点，且左子节点为空
        else if (null == targetNode.lNode)
        {
            if (isLeftChild)
            {
                targetNode.lNode.parent = targetNode;
                targetNode.lNode = targetNode.rNode;
            }
            else
            {
                targetNode.rNode.parent = targetNode;
                targetNode.rNode = targetNode.rNode;
            }
        }
        //4.删除节点有两个子节点且 后继节点是删除节点的右子级点
        else
        {
            BinaryTreeNode<T> succesor = GetSuccessor(targetNode);

            if (isLeftChild)
            {
                succesor.parent = targetNode;
                targetNode.lNode = succesor;
            }
            else
            {
                succesor.parent = targetNode;
                targetNode.rNode = succesor;
            }

        }

            return true;
    }


    public BinaryTreeNode<T> GetSuccessor(BinaryTreeNode<T> node)
    {
        //保留后继节点的父节点
        BinaryTreeNode<T> successorParent = node;
        //右节点的最后左节点 删除后的后继节点
        BinaryTreeNode<T> successor = node;

        //右节点的最后左节点可能为空
        BinaryTreeNode<T> AtLastLNode = node.rNode;

        while (null != AtLastLNode)
        {
            successorParent = successor;
            successor = AtLastLNode;
            AtLastLNode = AtLastLNode.lNode;
        }
        
        //替换节点 将移除后的节点改为此后继节点
        if (successor != node.rNode)
        {
            successorParent.lNode = successor.rNode;
            successorParent.lNode.parent = successorParent;

            successor.rNode = node.rNode;
            successor.rNode.parent = successor;
        }
        return successor;
    }


    public BinaryTreeNode<T> Find(T item)
    {
        foreach (BinaryTreeNode<T> node in MidTraversal(root))
        {
            if (null != node.value && node.value.Equals(item))
            {
                return node;
            }
        }
        return null;
    }

    #region 枚举器迭代器合集

    //先序遍历 根 左 右
    IEnumerable<BinaryTreeNode<T>> PerTraversal(BinaryTreeNode<T> _node)
    {

        yield return _node;

        if (null != _node.lNode)
        {
            foreach (BinaryTreeNode<T> item in PerTraversal(_node.lNode))
            {
                yield return item;
            }
        }

        if (null != _node.rNode)
        {
            foreach (BinaryTreeNode<T> item in PerTraversal(_node.rNode))
            {
                yield return item;
            }
        }

    }

    //中序遍历 左 根 右
    IEnumerable<BinaryTreeNode<T>> MidTraversal(BinaryTreeNode<T> _node)
    {
        if (null != _node.lNode)
        {
            foreach (BinaryTreeNode<T> item in MidTraversal(_node.lNode))
            {
                yield return item;
            }
        }

        yield return _node;

        if (null != _node.rNode)
        {
            foreach (BinaryTreeNode<T> item in MidTraversal(_node.rNode))
            {
                yield return item;
            }
        }

    }

    //中序遍历 左 根 右
    IEnumerable<BinaryTreeNode<T>> ProTraversal(BinaryTreeNode<T> _node)
    {
        if (null != _node.lNode)
        {
            foreach (BinaryTreeNode<T> item in ProTraversal(_node.lNode))
            {
                yield return item;
            }
        }

        if (null != _node.rNode)
        {
            foreach (BinaryTreeNode<T> item in ProTraversal(_node.rNode))
            {
                yield return item;
            }
        }

        yield return _node;

    }

    #endregion

    public void PrintAll()
    {
        foreach (BinaryTreeNode<T> item in MidTraversal(root))
        {
            PrintNode(item);
        }
    }

    public void PrintNode(BinaryTreeNode<T> node)
    {
        if (node == null) return;
        if (null != node.lNode && null != node.rNode)
        {
            Console.WriteLine("  data:" + node.value + " lvalue:" + node.lNode.value + " rvalue:" + node.rNode.value);
        }
        else if (null != node.lNode)
        {
            Console.WriteLine("  data:" + node.value + " lvalue:" + node.lNode.value);
        }
        else if (null != node.rNode)
        {
            Console.WriteLine("  data:" + node.value + " rvalue:" + node.rNode.value);
        }


    }





}


//树节点
public class BinaryTreeNode<T>
{
    //当前节点值
    public T value;

    //父节点
    public BinaryTreeNode<T> parent { get; set; }

    //左子节点
    public BinaryTreeNode<T> lNode { get; set; }

    //右子节点
    public BinaryTreeNode<T> rNode { get; set; }

    //是否为根节点
    public bool IsRoot { get { return lNode != null || rNode != null; } }
    //是否为叶节点
    public bool IsLeaf { get { return lNode == null && rNode == null; } }

    public BinaryTreeNode(T _value)
    {
        value = _value;
    }
    public BinaryTreeNode(T _value, BinaryTreeNode<T> _parent)
    {
        value = _value;
        parent = _parent;
    }
    public BinaryTreeNode(T _value, BinaryTreeNode<T> _parent, BinaryTreeNode<T> l, BinaryTreeNode<T> r)
    {
        value = _value;
        parent = _parent;
        lNode = l;
        rNode = r;
    }

    //析构 不知道有没有用
    public void Clear()
    {

    }

}



