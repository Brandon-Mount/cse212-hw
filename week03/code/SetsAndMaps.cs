using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Finds symmetric 2-letter word pairs in O(n) time.
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var wordSet = new HashSet<string>(words);
        var result = new HashSet<string>();

        foreach (var word in words)
        {
            if (word[0] == word[1]) continue; // Skip like "aa"

            var reversed = new string(new[] { word[1], word[0] });

            if (wordSet.Contains(reversed))
            {
                var pair = new[] { word, reversed };
                Array.Sort(pair);
                result.Add($"{pair[0]} & {pair[1]}");
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Summarizes how many people earned each degree in the file (column 4).
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');

            if (fields.Length >= 4)
            {
                var degree = fields[3].Trim();

                if (!string.IsNullOrEmpty(degree))
                {
                    if (!degrees.ContainsKey(degree))
                        degrees[degree] = 0;

                    degrees[degree]++;
                }
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determines if two words are anagrams (case and space insensitive).
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        string Normalize(string word) =>
            new string(word
                .ToLower()
                .Where(char.IsLetterOrDigit)
                .ToArray());

        var w1 = Normalize(word1);
        var w2 = Normalize(word2);

        if (w1.Length != w2.Length)
            return false;

        var count = new Dictionary<char, int>();

        foreach (var c in w1)
        {
            if (!count.ContainsKey(c))
                count[c] = 0;
            count[c]++;
        }

        foreach (var c in w2)
        {
            if (!count.ContainsKey(c)) return false;
            count[c]--;
            if (count[c] < 0) return false;
        }

        return true;
    }

    /// <summary>
    /// Fetches and returns earthquake locations and magnitudes from the USGS feed.
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();

        using var jsonStream = response.Content.ReadAsStream();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(jsonStream, options);

        var result = new List<string>();

        if (featureCollection?.Features != null)
        {
            foreach (var feature in featureCollection.Features)
            {
                var place = feature.Properties.Place;
                var mag = feature.Properties.Mag;
                result.Add($"{place} - Mag {mag}");
            }
        }

        return result.ToArray();
    }
}
