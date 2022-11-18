using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 孩子兄弟表示法实现树数据结构
/// </summary>
public class TreeLinkedList : ITree
{
    private object element;

    /// <summary>
    /// 父节点 子节点 兄弟节点
    /// </summary>
    private TreeLinkedList parent, firstChild, nextSibling;

    public TreeLinkedList(object _obj, TreeLinkedList _parent, TreeLinkedList _firstChild, TreeLinkedList _nextSibling)
    {
        element = _obj;
        parent = _parent;
        firstChild = _firstChild;
        nextSibling = _nextSibling;
    }

    /// <summary>
    /// 获取当前节点元素
    /// </summary>
    /// <returns></returns>
    public object GetElement()
    {
        return element;
    }

    /// <summary>
    /// 设置当前节点元素
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public object SetElement(object obj)
    {
        object bak = element;
        element = obj;
        return bak;
    }

    public TreeLinkedList GetParent()
    {
        return parent;
    }

    public TreeLinkedList GetFirstChild()
    {
        return firstChild;
    }

    public TreeLinkedList GetNextSibling()
    {
        return nextSibling;
    }

    public int GetSize()
    {
        int size = 1;//当前节点也算自己系
        TreeLinkedList subtree = firstChild;
        while (null !=subtree)
        {
            size += subtree.GetSize();
            subtree = subtree.GetNextSibling();
        }
        return size;
    }


    public int GetHeight()
    {
        int height = 0;
        TreeLinkedList subtree = firstChild;
        while (null != subtree)
        {
            height = Math.Max(height, subtree.GetHeight());
            subtree = subtree.GetNextSibling();
        }
        return height;
    }

    public int GetDepth()
    {
        int depth = 0;
        TreeLinkedList p = parent;
        while (null != p)
        {
            depth ++;
            p = p.GetParent();
        }
        return depth;
    }

}
