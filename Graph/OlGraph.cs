using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 弧信息
/// </summary>
class ArcInfo
{
    //弧起点 弧尾
    public int tailVex;
    //弧终点 弧头
    public int headvex;
    //弧头相同的弧信息
    public ArcInfo headLink;
    //弧尾相同的弧信息
    public ArcInfo tailLink;
    //其他信息
    public string other;
}

/// <summary>
/// 顶点节点
/// </summary>
class VexNode
{
    public char data;
    //弧头为本顶点的首个弧信息
    public ArcInfo firstIn;
    //弧尾为本顶点的首个弧信息
    public ArcInfo firstOut;

    public VexNode(char _data)
    {
        data = _data;
    }
}

/// <summary>
/// 构造用弧
/// </summary>
public class Edge
{
    public char start;
    public char end;
    public int weight;
    public Edge(char _start, char _end, int _weight)
    {
        start = _start;
        end = _end;
        weight = _weight;
    }

}

/// <summary>
/// 有向图
/// </summary>
public class OLGraph
{
    //顶点个数
    int vexCount;
    //弧个数
    int arcCount;
    //顶点信息
    VexNode[] vexs;

    /// <summary>
    /// 构造图（十字链表）
    /// </summary>
    /// <param name="vertexs">顶点信息</param>
    /// <param name="edges">构造弧信息</param>
    public OLGraph(char[] vertexs, Edge[] edges)
    {
        vexCount = vertexs.Length;
        arcCount = edges.Length;

        vexs = new VexNode[vexCount];
        //初始化顶点信息
        for (int i = 0; i < vexCount; i++)
        {
            vexs[i] = new VexNode(vertexs[i]);
        }

        char start, end;
        int startIndex, endIndex;

        //初始化边信息
        for (int i = 0; i < arcCount; i++)
        {
            start = edges[i].start;
            end = edges[i].end;

            startIndex = Array.FindIndex(vexs, (f) => f.data == start);
            endIndex = Array.FindIndex(vexs, (f) => f.data == end);

            //防止重复链接边信息
            if (CheckAddArcInfo(startIndex, edges[i], true))
            {
                //存储弧尾顶点信息
                ArcInfo arc1 = new ArcInfo();
                arc1.tailVex = startIndex;
                arc1.headvex = endIndex;
                arc1.tailLink = vexs[startIndex].firstOut;
                vexs[startIndex].firstOut = arc1;
            }

            //防止重复链接边信息
            if (CheckAddArcInfo(endIndex, edges[i], false))
            {
                //存储弧头顶点信息
                ArcInfo arc2 = new ArcInfo();
                arc2.tailVex = startIndex;
                arc2.headvex = endIndex;
                arc2.headLink = vexs[endIndex].firstIn;
                vexs[endIndex].firstIn = arc2;
            }

        }

    }

    /// <summary>
    /// 判断是否添加链接边信息
    /// </summary>
    /// <param name="index">顶点索引</param>
    /// <param name="edge">边信息</param>
    /// <param name="isTail">是否为弧尾</param>
    /// <returns></returns>
    private bool CheckAddArcInfo(int index, Edge edge, bool isTail)
    {
        if (vexs[index] == null) return false;
        if (vexs[index].firstOut == null && isTail) return true;
        if (vexs[index].firstIn == null && !isTail) return true;
        int startIndex = Array.FindIndex(vexs, (f) => f.data == edge.start);
        int endIndex = Array.FindIndex(vexs, (f) => f.data == edge.end);
        ArcInfo arc = isTail ? vexs[index].firstOut : vexs[index].firstIn;
        if (arc == null)
        {
            return true;
        }
        if (arc.tailVex == startIndex && arc.headvex == endIndex)
        {
            return false;
        }
        while (isTail ? arc.tailLink != null : arc.headLink != null)
        {
            arc = isTail ? arc.tailLink : arc.headLink;
            if (arc.tailVex == startIndex && arc.headvex == endIndex)
            {
                return false;
            }
        }
        return true;
    }


    public void PrintOLGraphInfo()
    {
        Console.WriteLine("打印该图信息：");
        ArcInfo arc = new ArcInfo();
        Console.WriteLine();
        Console.WriteLine("出度：");
        for (int i = 0; i < vexCount; i++)
        {
            //打印顶点
            Console.WriteLine();
            Console.Write("顶点信息:" + vexs[i].data + "-->");
            //打印链接表
            if (vexs[i].firstOut != null)
            {
                arc = vexs[i].firstOut;
                Console.Write(vexs[arc.headvex].data.ToString() + "-->");
                while (arc.tailLink != null)
                {
                    arc = arc.tailLink;
                    Console.Write(vexs[arc.headvex].data.ToString() + "-->");
                }
                Console.Write("Null");
                Console.WriteLine();
            }
            else
            {
                Console.Write("Null");
                Console.WriteLine();
            }
        }

        Console.WriteLine();
        Console.WriteLine("入度：");
        for (int i = 0; i < vexCount; i++)
        {
            //打印顶点
            Console.WriteLine();
            Console.Write("顶点信息:" + vexs[i].data + "-->");
            //打印链接表
            if (vexs[i].firstIn != null)
            {
                arc = vexs[i].firstIn;
                Console.Write(vexs[arc.tailVex].data.ToString() + "-->");
                while (arc.headLink != null)
                {
                    arc = arc.headLink;
                    Console.Write(vexs[arc.tailVex].data.ToString() + "-->");
                }
                Console.Write("Null");
                Console.WriteLine();
            }
            else
            {
                Console.Write("Null");
                Console.WriteLine();
            }
        }

    }

    private void PrintConsole(string str)
    {
        Console.WriteLine(str);
    }

}
