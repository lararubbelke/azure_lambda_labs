namespace PartsUnlimitedDataGen
{
    public class EventMessage
    {
        public string EventDate { get; set; }
        public string Type { get; set; }

        public int ProductId { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public override string ToString()
        {
            return string.Format("**{0}*{1}*{2}*{3}**", Type, ProductId, Title, Category);
        }
    }
}
