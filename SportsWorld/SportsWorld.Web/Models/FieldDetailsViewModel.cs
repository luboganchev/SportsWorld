namespace SportsWorld.Web.Models
{
    using AutoMapper;
    using SportsWorld.Models;
    using SportsWorld.Web.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class FieldDetailsViewModel : BaseFieldViewModel
    {
        public FieldDetailsViewModel()
        {
            Comments = new HashSet<Comment>();
        }

        public string Description { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}