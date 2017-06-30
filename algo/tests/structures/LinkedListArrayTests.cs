﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using algorithms.structures;
using System.Linq;

namespace tests.structures
{
    [TestClass]
    public class LinkedListArrayTests
    {
        [TestMethod]
        public void TestM_LinkedListArray()
        {
            // arrange
            int N = 10;
            int M = 1000;
            long[] A = Enumerable.Range(1, M / N).Select(p => (long)p).ToArray();
            Random rnd = new Random();

            LinkedListArraySet<long> llas = new LinkedListArraySet<long>(M);
            LinkedListArray<long>[] lla = new LinkedListArray<long>[N];
            for (int i = 0; i < N / 2; i++)
                lla[i] = llas.CreateLinkedListArray(A);
            for (int i = N / 2; i < N; i++)
            {
                lla[i] = llas.CreateLinkedListArray();
                for (int j = 0; j < M / N; j++) lla[i].AddAfter(j + 1);
            }

            int count1 = 0;
            for (int i = 0; i < N; i++) count1 += lla[i].Count;

            // act
            for (int i = 0; i < 100000; i++)
            {
                int j = rnd.Next(N);
                int cnt = lla[j].Count;
                if (cnt == 0) continue;

                lla[j].First();
                int k = rnd.Next(cnt);
                while (k-- > 0 && lla[j].Next()) ;
                long x = lla[j].Current;
                lla[j].Remove();

                j = rnd.Next(N);
                lla[j].First();
                cnt = lla[j].Count;
                k = cnt > 0 ? rnd.Next(cnt) : 0;
                while (k-- > 0 && lla[j].Next()) ;
                lla[j].AddAfter(x);
            }
            int count2 = 0;
            for (int i = 0; i < N; i++) count2 += lla[i].Count;

            // assert
            Assert.AreEqual(count1, count2, "LinkedListArray.LinkedListArray is not correct");
        }
    }
}