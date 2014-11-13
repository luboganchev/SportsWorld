namespace SportsWorld.Web.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public static class HtmlExtensions
    {
        public static MvcHtmlString Rating(this HtmlHelper helper, float value, int scale)
        {
            if (value > scale)
            {
                throw new ArgumentException("Scale should be bigger than value");
            }

            var span = new TagBuilder("span");
            var fullStar = new TagBuilder("span");
            fullStar.AddCssClass("glyphicon");
            fullStar.AddCssClass("glyphicon-star");

            int integerValue = Convert.ToInt32(value);
            for (int i = 0; i < integerValue; i++)
            {
                span.InnerHtml += fullStar;
            }

            var emptyStar = new TagBuilder("span");
            emptyStar.AddCssClass("glyphicon");
            emptyStar.AddCssClass("glyphicon-star-empty");

            int emptyStarsCount = scale - integerValue;
            for (int i = 0; i < emptyStarsCount; i++)
            {
                span.InnerHtml += emptyStar;
            }

            return new MvcHtmlString(span.ToString());
        }
    }
}