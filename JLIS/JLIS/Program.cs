using System;
using System.Collections.Generic;

namespace JLIS
{
    class Program
    {
        static int n;
        static int[] cache = new int[101];
        static int[] S = new int[100];

        static int jlisN;
        static int jlisM;
        static int[] jlisA = new int[100];
        static int[] jlisB = new int[100];
        static int[,] jlisCache = new int[101,101];
        const Int64 NEGINF = Int64.MinValue;

        static void Main(string[] args)
        {
            for (int i = 0; i < 101; i++)
                cache[i] = -1;
        }

        int lis(List<int> A)
        {
            if (A == null || A.Count == 0) return 0;

            int ret = 0;
            for(int i = 0; i < A.Count; i++)
            {
                List<int> B = new List<int>();
                for(int j = i+1; j < A.Count; j++)
                {
                    if(A[i] < A[j])
                    {
                        B.Add(A[j]);
                    }
                }
                ret = ret > 1 + lis(B) ? ret : 1 + lis(B);
            }

            return ret;
        }

        int lis2(int start)
        {
            int ret = cache[start];
            if (ret != -1) return ret;

            ret = 1;
            for(int next = start+1; next < n; next++)
            {
                if (S[start] < S[next])
                    return ret > lis2(next) + 1 ? ret : lis2(next) + 1;
            }

            return ret;
        }

        int lis3(int start)
        {
            int ret = cache[start + 1];
            if (ret != -1) return ret;

            ret = 1;
            for(int next = start+1; next < n; next++)
            {
                if(start == -1 || S[start] < S[next])
                {
                    return ret > lis3(next) + 1 ? ret : lis3(next) + 1;
                }
            }

            return ret;
        }

        int jlis(int indexA, int indexB)
        {
            int ret = jlisCache[indexA+1, indexB+1];
            if(ret != -1)
            {
                return ret;
            }
            
            ret = 2;
            Int64 a = (indexA == -1 ? NEGINF : jlisA[indexA]);
            Int64 b = (indexB == -1 ? NEGINF : jlisB[indexB]);
            Int64 maxElement = a > b ? a : b;

            for(int nextA = indexA + 1; nextA < jlisN; nextA++)
            {
                if(maxElement < jlisA[nextA])
                {
                    ret = ret > jlis(nextA, indexB) + 1 ? ret : jlis(nextA, indexB) + 1;
                }
            }
            for(int nextB = indexB + 1; nextB < jlisM; nextB++)
            {
                if(maxElement < jlisB[nextB])
                {
                    ret = ret > jlis(indexA, nextB) + 1 ? ret : jlis(indexA, nextB) + 1;
                }
            }

            return ret;
        }
    }
}
