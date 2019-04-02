using WHMSData.Contracts;

namespace WHMSData.Models
{
    public class Address : Model
    {
        public string Text { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }
    }
}