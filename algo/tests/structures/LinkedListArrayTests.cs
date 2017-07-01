using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using algorithms.structures;
using System.Linq;
using System.Collections.Generic;

namespace tests.structures
{
    [TestClass]
    public class LinkedListArrayTests
    {
        [TestMethod]
        public void Test_LinkedListArray()
        {
            // arrange
            int N = 10;
            int M = 1000;
            long[] A = Enumerable.Range(1, M / N).Select(p => (long)p).ToArray();
            Random rnd = new Random();

            LinkedListArraySet<long> llas = new LinkedListArraySet<long>(M);
            LinkedListArray<long>[] lla = new LinkedListArray<long>[N];
            List<long>[] lst = new List<long>[N];
            for (int i = 0; i < N / 2; i++)
            {
                lla[i] = llas.CreateLinkedListArray(A);
                lst[i] = new List<long>(A);
            }
            for (int i = N / 2; i < N; i++)
            {
                lla[i] = llas.CreateLinkedListArray();
                lst[i] = new List<long>();
                for (int j = 0; j < M / N; j++)
                {
                    lla[i].AddAfter(j + 1);
                    lst[i].Add(j + 1);
                }
            }

            int count1 = 0;
            for (int i = 0; i < N; i++) count1 += lla[i].Count;

            // act
            for (int i = 0; i < 100000; i++)
            {
                int j = rnd.Next(N);
                int cnt = lla[j].Count;
                if (cnt < 2) continue;

                lla[j].First();
                int k = rnd.Next(cnt);
                int k1 = k;
                while (k-- > 0) lla[j].Next();
                long x = lla[j].Current;
                long x1 = lst[j][k1];
                lla[j].Remove();
                lst[j].RemoveAt(k1);

                j = rnd.Next(N);
                lla[j].First();
                cnt = lla[j].Count;
                k = rnd.Next(cnt);
                k1 = k;
                while (k-- > 0) lla[j].Next();
                lla[j].AddAfter(x);
                lst[j].Insert(k1 + 1, x);
            }
            int count2 = 0;
            for (int i = 0; i < N; i++) count2 += lla[i].Count;

            // assert
            for (int i = 0; i < N; i++)
            {
                long[] lst2 = lla[i].GetItems().ToArray();

                Assert.AreEqual(lla[i].Count, lst[i].Count, "Test_LinkedListArray FAILED.");
                Assert.AreEqual(lst2.Length, lst[i].Count, "Test_LinkedListArray FAILED.");

                for (int j = 0; j < lla[i].Count; j++) { 
                    Assert.AreEqual(lst2[j], lst[i][j], "Test_LinkedListArray FAILED.");
                }
            }
        }
    }
}
