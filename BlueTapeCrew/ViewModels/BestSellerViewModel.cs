namespace BlueTapeCrew.ViewModels
{
    public class BestSellerViewModel
    {
        public BestSellerViewModel(int id, string name, string linkName, string imgSource, string price)
        {
            Id = id;
            Name = name;
            LinkName = linkName;
            ImgSource = imgSource;
            Price = price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LinkName { get; set; }
        public string ImgSource { get; set; }
        public string Price { get; set; }
    }
}