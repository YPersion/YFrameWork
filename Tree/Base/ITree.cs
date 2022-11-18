using System;

public interface ITree
{
    /// <summary>
    /// 获取当前节点处的元素
    /// </summary>
    /// <returns></returns>
    object GetElement();

    /// <summary>
    /// 将对象obj存入当前节点，并返回此前的内容
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    object SetElement(Object obj);

    /// <summary>
    /// 返回当前节点的父节点
    /// </summary>
    /// <returns></returns>
    TreeLinkedList GetParent();

    /// <summary>
    /// 返回当前节点的长子
    /// </summary>
    /// <returns></returns>
    TreeLinkedList GetFirstChild();

    /// <summary>
    /// 返回当前节点的最大弟弟 ？？
    /// </summary>
    /// <returns></returns>
    TreeLinkedList GetNextSibling();

    /// <summary>
    /// 返回节点后的元素数目
    /// </summary>
    /// <returns></returns>
    int GetSize();

    /// <summary>
    /// 返回当前节点的高度
    /// </summary>
    /// <returns></returns>
    int GetHeight();

    /// <summary>
    /// 返回当前节点的深度
    /// </summary>
    /// <returns></returns>
    int GetDepth();

}