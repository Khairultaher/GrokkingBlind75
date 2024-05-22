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
}
