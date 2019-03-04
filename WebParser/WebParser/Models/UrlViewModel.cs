using System;
using System.ComponentModel.DataAnnotations;

namespace WebParser.Models
{
    public class UrlViewModel
    {
        [Required]
        [Url]
        public string Url { get; set; }
    }
}