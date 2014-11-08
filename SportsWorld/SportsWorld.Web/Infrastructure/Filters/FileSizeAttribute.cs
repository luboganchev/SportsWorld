namespace SportsWorld.Web.Infrastructure.Filters
{
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public class FileSizeAttribute : ValidationAttribute
    {
        private readonly int maxSize;

        public FileSizeAttribute(int maxSize)
        {
            this.maxSize = maxSize;
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            return (value as HttpPostedFileBase).ContentLength <= this.maxSize;
        }

        public override string FormatErrorMessage(string name)
        {
            float maxSizeInMB = this.maxSize / 1024;
            return string.Format("The file size should not exceed {0} kb", maxSizeInMB);
        }
    }
}