using SQLite;
using System;

namespace UnitConverter.Models
{
    public class Conversion
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string FromUnit { get; set; }
        public string ToUnit { get; set; }
        public double InputValue { get; set; }
        public double ResultValue { get; set; }
        public DateTime ConvertedAt { get; set; }
    }
}
