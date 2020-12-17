using BeautyStore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BeautyStore.Interfaces.Repositories
{
    public interface IPhotoRepository : IBaseRepository<Photo>
    {
        Task<IDictionary<int,Photo>> GetPhotosByProductId(Guid productId, bool onlyCoverPhoto = false);
        void RemoveRange(IEnumerable<Photo> photos);
    }
}
