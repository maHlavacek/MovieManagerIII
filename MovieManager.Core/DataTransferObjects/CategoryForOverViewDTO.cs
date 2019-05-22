using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MovieManager.Core.DataTransferObjects
{
    public class CategoryForOverViewDTO
    {
        [Display(Name = "Available movies")]
        public int Count { get; set; }
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Avarage duration")]
        public int AVGDuration { get; set; }
        public int Id { get; set; }
    }
}
