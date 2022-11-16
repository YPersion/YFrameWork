using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 顶点信息
/// </summary>
public class AMLVertex
{
    public char data;
    public AMLEdge firstEdge;
    public AMLVertex(char _data)
    {
        data = _data;
    }
}

/// <summary>
/// 边信息
/// </summary>
public class AMLEdge
{
    public int iVex;
    public AMLEdge iLink;
    public int jVex;
    public AMLEdge jLink;
    public AMLEdge(int _iVex, int _jVex)
    {
        iVex = _iVex;
        jVex = _jVex;
    }
}

/// <summary>
/// 构造用边
/// </summary>
public class AMLEdgeNode
{
    public char iData;
    public char jData;
    public AMLEdgeNode(char _iData, char _jData)
    {
        iData = _iData;
        jData = _jData;
    }
}


/// <summary>
/// 邻接多重表，储存无向图
/// </summary>
public class AMLGraph
{
    //顶点个数
    int vexCount;
    //弧个数
    int edgeCount;
    //顶点信息
    AMLVertex[] vexs;

    public AMLGraph(char[] vertexs, AMLEdgeNode[] edge)
    {
        vexCount = vertexs.Length;
        if (vexCount == 0) return;
        edgeCount = edge.Length;

        vexs = new AMLVertex[vexCount];
        for (int i = 0; i < vexCount; i++)
        {
            vexs[i] = new AMLVertex(vertexs[i]);
        }

        char iData, jData;
        int iIndex, jIndex;
        (bool iAdd, bool jAdd) isAdd;
        for (int i = 0; i < edgeCount; i++)
        {
            iData = edge[i].iData;
            jData = edge[i].jData;

            iIndex = Array.FindIndex(vexs, (f) => { return f.data == iData; });
            jIndex = Array.FindIndex(vexs, (f) => { return f.data == jData; });
            isAdd = CheckAddEdge(iIndex, jIndex);
            if (!isAdd.iAdd && !isAdd.jAdd) continue;
             
            AMLEdge tempEdge = new AMLEdge(iIndex, jIndex);
            if (isAdd.iAdd)
            {
                //头插法
                tempEdge.iLink = vexs[iIndex].firstEdge;
                vexs[iIndex].firstEdge = tempEdge;
            }
            else if (isAdd.jAdd)
            {
                //头插法
                tempEdge.jLink = vexs[jIndex].firstEdge;
                vexs[jIndex].firstEdge = tempEdge;
            }
        }
    }

    /// <summary>
    /// 是否可插入边
    /// </summary>
    /// <returns></returns>
    private (bool iAdd, bool jAdd) CheckAddEdge(int startIndex,int endIndex)
    {
        (bool iAdd, bool jAdd) addCheck = (true, true);
        addCheck.iAdd = CheckAddEdge(startIndex, endIndex, vexs[startIndex]);
        addCheck.jAdd = CheckAddEdge(startIndex, endIndex, vexs[endIndex]);
        return addCheck;
    }

    /// <summary>
    /// 判断单条边的插入
    /// </summary>
    /// <param name="iIndex"></param>
    /// <param name="jIndex"></param>
    /// <param name="vertex"></param>
    /// <returns></returns>
    private bool CheckAddEdge(int iIndex, int jIndex, AMLVertex vertex)
    {
        if (vertex == null) return false;
        if (vertex.firstEdge == null) return true;
        AMLEdge cursor = vertex.firstEdge;
        while (cursor != null)
        {
            if (cursor.iVex == iIndex && cursor.jVex == jIndex)
            {
                return false;
            }
            if (cursor.iVex == jIndex && cursor.jVex == iIndex)
            {
                return false;
            }

            if (cursor.iVex == iIndex)
            {
                cursor = cursor.iLink;
            }
            else
            {
                cursor = cursor.jLink;
            }
        }
        return true;
    }


    public void print()
    {
        for (int i = 0; i < vexCount; i++)
        {
            Console.WriteLine("顶点 " + vexs[i].data + " 的所有边: ");
            int vIndex = Array.FindIndex(vexs, (f) => { return f.data == vexs[i].data; });
            AMLEdge cursor = vexs[i].firstEdge;
            while (cursor != null)
            {
                Console.WriteLine(vexs[cursor.iVex].data + "---" + vexs[cursor.jVex].data);
                if (cursor.iVex == vIndex)
                {
                    cursor = cursor.iLink;
                }
                else
                {
                    cursor = cursor.jLink;
                }
            }

            Console.WriteLine();
        }
    }

}
