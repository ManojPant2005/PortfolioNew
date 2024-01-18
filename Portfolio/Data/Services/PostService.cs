using Microsoft.EntityFrameworkCore;
using Portfolio.Data.Configuration;

namespace Portfolio.Data.Services
{
    public class PostService
    {
        private readonly AppDbContext _context;

        public PostService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewPost>> GetPostsAsync(bool publishedOnly = false, string? categorySlug = null)
        {
            var query = _context.NewPosts
                            .Include(bp => bp.Category)
                           .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(categorySlug))
            {
                var categoryId = await _context.Categories
                                    .AsNoTracking()
                                    .Where(c => c.Slug == categorySlug)
                                    .Select(c => c.Id)
                                    .FirstOrDefaultAsync();
                if (categoryId > 0)
                {
                    query = query.Where(bp => bp.CategoryId == categoryId);
                }
            }

            if (publishedOnly)
            {
                query = query.Where(bp => bp.IsPublished);
            }

            return await query.ToListAsync();
        }

        public async Task<PostSaveModel?> GetPostAsync(int blogPostId) =>
            await _context.NewPosts
                        .Include(bp => bp.Category)
                        .AsNoTracking()
                        .Select(PostSaveModel.Selector)
                        .FirstOrDefaultAsync(bp => bp.Id == blogPostId);

        public async Task<MethodResult> SaveAsync(PostSaveModel post, int userId)
        {

            if (post.Id == 0)
            {
                var entity = post.ToBlogEntity(userId);
                // Creating a new blog post
                entity.Slug = entity.Slug.Slugify();

                entity.CreatedOn = DateTime.Now;
                if (entity.IsPublished)
                {
                    entity.PublishedOn = DateTime.Now;
                }

                await _context.AddAsync(entity);
            }
            else
            {
                // Updating an existing blog post

                NewPost? entity = await _context.NewPosts
                                    .FirstOrDefaultAsync(bp => bp.Id == post.Id);
                if (entity is not null)
                {
                    var wasBlogPostPublished = entity.IsPublished;

                    entity = post.Merge(entity);

                    entity.ModifiedOn = DateTime.Now;

                    if (entity.IsPublished)
                    {
                        if (wasBlogPostPublished)
                        {
                            // Do nothing
                        }
                        else
                        {
                            // The blog post was not publsihed in the db
                            // but user published it fromthe ui now
                            entity.PublishedOn = DateTime.Now;
                        }
                    }
                    else if (wasBlogPostPublished)
                    {
                        // This blog post was published earlier in the db
                        // but user now un-published it from the ui

                        entity.PublishedOn = null;
                    }
                }
                else
                {
                    return MethodResult.Failure("This blog post does not exist");
                }
            }

            try
            {
                if (await _context.SaveChangesAsync() > 0)
                {
                    return MethodResult.Succes();
                }
                else
                    return MethodResult.Failure("Unknown error occurred while saving the blog post");
            }
            catch (Exception ex)
            {
                return MethodResult.Failure(ex.Message);
                //throw;
            }
        }

        public async Task<NewPost?> GetPostBySlugAsync(string slug) =>
            await _context.NewPosts
                        .Include(bp => bp.Category)
                        .AsNoTracking()
                        .FirstOrDefaultAsync(bp => bp.IsPublished && bp.Slug == slug);
    }
}
