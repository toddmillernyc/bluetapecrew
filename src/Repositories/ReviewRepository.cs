using System;
using System.Threading.Tasks;
using Repositories.Entities;
using Repositories.Interfaces;

namespace Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly BtcEntities _db;

        public ReviewRepository(BtcEntities db)
        {
            _db = db;
        }

        public async Task Create(Review review)
        {
            review.DateCreated = DateTime.Now;
            _db.Reviews.Add(review);
            await _db.SaveChangesAsync();
        }
    }
}