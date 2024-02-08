namespace MovieApp.Dtos.Movie
{
    public class MovieListResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BannerUrl { get; set; }
        public string MovieYear { get; set; }
        public string? Description { get; set; }
        public string FullBannerUrl => !string.IsNullOrEmpty(BannerUrl) ? "/moviePhoto/" + BannerUrl : string.Empty;
    }
    public class MovieDetailResponseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string BannerUrl { get; set; }
        public string MovieYear { get; set; }
        public string? Description { get; set; }
        public string FullBannerUrl => !string.IsNullOrEmpty(BannerUrl) ? "/moviePhoto/" + BannerUrl : string.Empty;
    }
}