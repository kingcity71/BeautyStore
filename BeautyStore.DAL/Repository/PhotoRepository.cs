using BeautyStore.Entities;
using BeautyStore.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautyStore.DAL.Repository
{
    public class PhotoRepository : BaseRepository<Photo>, IPhotoRepository
    {
        private readonly IProductPhotoRepository _productPhotoRepository;

        public PhotoRepository(IProductPhotoRepository productPhotoRepository, IConfiguration configuration) : base(configuration)
        {
            _productPhotoRepository = productPhotoRepository;
        }

        public async Task<IDictionary<int, Photo>> GetPhotosByProductId(Guid productId, bool onlyCoverPhoto = false)
        {
            var dic = new Dictionary<int, Photo>();
            using var context = GetContext();
            var productPhotos = await _productPhotoRepository.GetItemsByProductId(productId);

            var photoIds =
                onlyCoverPhoto ?
                productPhotos.Where(x => x.DisplayOrder == 0).Select(x => x.PhotoId).ToList()
                : productPhotos.Select(x => x.PhotoId).ToList();

            var photos = await context.Photos
                .AsNoTracking()
                .Where(photo => photoIds.Contains(photo.Id))
                .ToListAsync();

            foreach (var photo in photos)
            {
                var order = productPhotos.FirstOrDefault(x => x.PhotoId == photo.Id).DisplayOrder;
                dic.Add(order, photo);
            }
            return dic;
        }

        public void RemoveRange(IEnumerable<Photo> photos)
        {
            using var context = GetContext();
            context.RemoveRange(photos);
        }
    }
}
