namespace PHIASPACE.TRAINING.Models{
    public class GridTableModel{
        public int limit { get; set; }
        public int offset { get; set; }
        public string direction { get; set; }
        public string order { get; set; }
        public string search { get; set; }
        public int? page { get; set; }
    }
}