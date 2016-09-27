namespace PartsUnlimitedDataGen
{
    public class EventMessage
    {
        public string EventDate { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; } 

        public override string ToString()
        {
            return string.Format("**{0}*{1}*{2}*{3}*{4}**", EventDate, UserId, Type, ProductId, Price);
        }
    }
}
