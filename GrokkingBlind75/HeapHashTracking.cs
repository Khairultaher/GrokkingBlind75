using Shouldly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrokkingBlind75;
public class HeapHashTracking
{
    [Theory]
    [InlineData("listen", "silent", true)]
    [InlineData("race", "cares", false)]
    public void ValidAnagramTest(string str1, string str2, bool expected) {
        bool result = true;
        if (str1.Length != str2.Length) {
            result = false;
        }
        Dictionary<char, int> keyValuePairs = new Dictionary<char, int>();

        foreach (char c in str1) {
            if (keyValuePairs.TryGetValue(c, out int value)) {
                keyValuePairs[c] = value + 1;
            }
            else {
                keyValuePairs.Add(c, 1);
            }
        }

        foreach (char c in str2) {
            if (keyValuePairs.TryGetValue(c, out int value)) {
                keyValuePairs[c] = value - 1;
            }
            else {
                result = false;
            }
        }

        foreach (var c in keyValuePairs) {
            if (c.Value != 0) { 
                result = false;
            }
        }

        result.ShouldBe(expected);
    }

    [Theory]
    [InlineData(new string[] { "eat", "tea", "beat", "neat" }, 0)]
    public void GroupAnagramsTest(string[] strs, int dumm) {

        var result = GroupAnagrams(strs);
    }

    public List<List<string>> GroupAnagrams(string[] strs) {
        if(strs.Length == 0) return new List<List<string>>();

        int[] count = new int[26];
        List<List<string>> arrayList = new List<List<string>> ();
        Dictionary<string, List<string>> keyValuePairs = new Dictionary<string, List<string>>();
        foreach (string str in strs) {
            Array.Fill(count, 0);
            foreach (char c in str.ToCharArray()) {
                int index = c - 'a';
                count[index]++;

            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < count.Length; i++) {
                sb.Append("#");
                sb.Append(count[i]);
            }

            string key = sb.ToString();
            if (!keyValuePairs.ContainsKey(key))
                keyValuePairs.Add(key, new List<string>());

            keyValuePairs[key].Add(str);
        }

        arrayList.AddRange(keyValuePairs.Values);
        return arrayList;
    }


    [Theory]
    [InlineData(new int[] { 1, 1, 1, 2, 2, 3 }, 2)]
    public void TopKFrequent(int[] arr, int k) {
        List<int> expected = new List<int> { 1, 2 };

        Dictionary<int, int> numFrequencyMap = new Dictionary<int, int>();

        foreach (int n in arr)
            numFrequencyMap[n] = numFrequencyMap.GetValueOrDefault(n, 0) + 1;

        PriorityQueue<KeyValuePair<int, int>, int> topKElements = new PriorityQueue<KeyValuePair<int, int>, int>(
            Comparer<int>.Create((a, b) => a - b)
        );

        foreach (var entry in numFrequencyMap) {
            topKElements.Enqueue(entry, entry.Value);
            if (topKElements.Count > k)
                topKElements.Dequeue();
        }

        List<int> topNumbers = new List<int>(k);
        while (topKElements.Count > 0)
            topNumbers.Add(topKElements.Dequeue().Key);

        topNumbers.Sort();

        topNumbers.ShouldBe(expected);
    }

    [Theory]
    [InlineData(new int[] { 1, 2, 3, 4, 5}, 3)]
    public void MedianOfStream(int[] arr, int expected) { 
        PriorityQueue<int, int> maxHeap = new PriorityQueue<int, int>(
            Comparer<int>.Create((a, b) => b - a)
        );
        PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>(
            //Comparer<int>.Create((a, b) => a - b)
        );
        foreach (int num in arr) {
            if (maxHeap.Count == 0 || maxHeap.Peek() >= num)
                maxHeap.Enqueue(num, num);
            else
                minHeap.Enqueue(num, num);

            if (maxHeap.Count > minHeap.Count + 1) {
                var x = maxHeap.Dequeue();
                minHeap.Enqueue(x, x);
            }
            else if (maxHeap.Count < minHeap.Count) {
                var x = minHeap.Dequeue();
                maxHeap.Enqueue(x,x);
            }
                
        }
        double result = 0;
        if (maxHeap.Count == minHeap.Count)
            result = (maxHeap.Peek() + minHeap.Peek()) / 2.0;
        else
            result = maxHeap.Peek();

        result.ShouldBe(expected);
    }
}
