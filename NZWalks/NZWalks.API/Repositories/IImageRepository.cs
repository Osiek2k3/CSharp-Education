using NZWalks.API.Models.Domian;

namespace NZWalks.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image>Upload(Image image);
    }
}
