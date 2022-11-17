/// <summary>
/// KMP 算法 用于匹配 
/// </summary>
public class KMPArithmetic
{
    public static int KMP(string orgin, string target)
    {
        int[] next = GetNext(target);

        //for (int K = 0; K < next.Length; K++)
        //{
        //    Console.WriteLine(K.ToString() + "=>" + next[K]);
        //}
        //Console.WriteLine("===========================================");
        int i = 0;
        int j = 0;
        while (i < orgin.Length && j < target.Length)
        {
           // Console.WriteLine(i.ToString() + "=>" + j);
            if (j == -1 || orgin[i] == target[j])
            {
                ++i;
                ++j;
            }
            else
            {
                //定位到应该比较的的串索引
                j = next[j];
            }
        }
        if (j == target.Length)
        {
            //获取首个索引
            return i - target.Length;
        }


        return -1;
    }

    /// <summary>
    /// 计算定位串的初始比较索引
    /// 当定位当前索引的时候，存储的一定是下次匹配未匹配到，而当前被匹配时的索引值
    /// 没法写的太清晰，如果忘了，想知道推导逻辑 还是自己动笔画一画吧
    /// next[j+1]<=next[j]+1
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static int[] GetNext(string str)
    {
        int[] next = new int[str.Length];
        int i = 0;
        int j = -1;
        next[0] = -1;
        while (i < str.Length - 1)
        {
            if (j == -1 || str[i] == str[j])
            {

                next[++i] = ++j;
            }
            else
            {
                j = next[j];
            }
        }
        return next;
    }


}
