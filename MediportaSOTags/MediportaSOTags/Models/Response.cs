﻿namespace MediportaSOTags.Models
{
    public class Response
    {
        public IList<Item> items { get; set; }
        public bool has_more { get; set; }
        public int quota_max { get; set; }
        public int quota_remaining { get; set; }
    }
}
