using System;
using BlueTapeCrew.Data;
using BlueTapeCrew.Repositories.Interfaces;
using Entities;
using System.Threading.Tasks;

namespace BlueTapeCrew.Repositories
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