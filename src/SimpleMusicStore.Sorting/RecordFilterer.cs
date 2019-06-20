using SimpleMusicStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleMusicStore.Sorting
{
    public static class RecordFilters
    {
        public static IEnumerable<Record> FilterByGenre(this IEnumerable<Record> records, IEnumerable<string> genres)
        {
            if (genres.Any())
                return records.Where(r => genres.Contains(r.Genre));
            else
                return records;
        }

        public static IEnumerable<Record> FilterByFormat(this IEnumerable<Record> records, IEnumerable<string> formats)
        {
            if (formats.Any())
                return records.Where(r => formats.Contains(r.Format));
            else
                return records;
        }

        public static IEnumerable<Record> FilterByKeywords(this IEnumerable<Record> records, string[] keywords)
        {
            return records.Where(r =>
                keywords.Any(kw => r.Title.ToLower().Contains(kw)) ||
                keywords.Any(kw => r.Artist.Name.ToLower().Contains(kw)) ||
                keywords.Any(kw => r.Label.Name.ToLower().Contains(kw)));
        }
    }
}
