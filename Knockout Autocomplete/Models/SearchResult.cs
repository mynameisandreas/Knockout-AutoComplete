using System;

namespace Knockout_Autocomplete.Models
{
    public class SearchResult
    {
        public SearchResult()
        {
            
        }

        public SearchResult(string firstName, string lastName)
        {
            this.Id = Guid.NewGuid();
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Label = string.Format("{0} {1}", firstName, lastName);
        }

        public Guid Id { get; set; }
        public string Label { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}