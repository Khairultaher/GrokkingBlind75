using Shouldly;
using System;
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
}
