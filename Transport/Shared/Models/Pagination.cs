using System.Collections.Generic;
using System.Web;
using Microsoft.AspNetCore.WebUtilities;

namespace OnTest.Blazor.Transport.Shared.Models
{
    public class Pagination
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }
        public string SortLabel { get; set; }
        public string SortDirection { get; set; }

        public string BuildUrl(string uri)
        {
            var parameters = new Dictionary<string, string>
            {
                { "page", this.Page.ToString() ?? "1" },
                { "page_size", this.PageSize.ToString() ?? "10" },
                { "quesry", this.Query ?? ""},
                { "sort", this.SortLabel ?? ""},
                { "sort_dir", this.SortLabel ?? "" }
            };

            return QueryHelpers.AddQueryString(uri, parameters);
        }
    }
}