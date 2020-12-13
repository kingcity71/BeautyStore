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

        public PhotoRepository(IProductPhotoRepository productPhotoRepository, IConfiguration configuration) : base(configuration) {
            _productPhotoRepository = productPhotoRepository;
        }

        public async Task<IDictionary<int, Photo>> GetPhotosByProductId(Guid productId)
        {
            var dic = new Dictionary<int, Photo>();
            using var context = GetContext();
            var productPhotos = await _productPhotoRepository.GetItemsByProductId(productId);

            var photoIds = productPhotos.Select(x => x.Id).ToList();

            var photos = await context.Photos
                .AsNoTracking()
                .Where(photo => photoIds.Contains(photo.Id))
                .ToListAsync();

            foreach(var photo in photos)
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
