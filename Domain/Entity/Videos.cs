﻿using System.ComponentModel.DataAnnotations;

namespace AFEXChile.Domain.Entity
{
    public class Videos
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Url { get; set; }
        public string Duration { get; set; }
    }
}