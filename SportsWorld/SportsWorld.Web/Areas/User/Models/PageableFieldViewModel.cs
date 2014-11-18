namespace SportsWorld.Web.Areas.User.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class PageableFieldViewModel
    {
        public PageableFieldViewModel()
        {
            this.Fields = new HashSet<FieldViewModel>();
        }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public IEnumerable<FieldViewModel> Fields { get; set; }
    }
}