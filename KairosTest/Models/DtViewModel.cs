using System;
using System.Collections.Generic;

namespace KairosTest.Models
{
    public class DtViewModel
    {
        public int? Draw { get; set; }
        public int? Start { get; set; }
        public int? Length { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual Search Search { get; set; }
        public virtual List<Order> Order { get; set; }
        public virtual List<Column> Columns { get; set; }
    }

    public class Column
    {
        public string Data { get; set; }
        public string Name { get; set; }
        public bool? Searchable { get; set; }
        public bool? Orderable { get; set; }
        public virtual Search Search { get; set; }
    }

    public class Search
    {
        public string Value { get; set; }
        public bool? Regex { get; set; }
    }

    public class Order
    {
        public int? Column { get; set; }
        public string Dir { get; set; }
    }

    public class ResultSewaBuku
    {
        public int? Draw { get; set; }
        public int? RecordsTotal { get; set; }
        public int? RecordsFiltered { get; set; }
        public virtual IEnumerable<SewaBukuViewModel> Data { get; set; }
        public string Error { get; set; }
    }
}
