using Portfolio.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Portfolio.Data.Module
{
    public class PostSaveModel
    {
        public int Id { get; set; }

        [Required, MaxLength(120)]
        public string Title { get; set; }

        [Required, MaxLength(150)]
        public string Slug { get; set; }

        public int CategoryId { get; set; }

        [Required, MaxLength(250)]
        public string Introduction { get; set; }

        public string? Content { get; set; }

        public bool IsPublished { get; set; }

        public NewPost ToBlogEntity(int userId) =>
            new()
            {
                Id = Id,
                Title = Title,
                Slug = Slug,
                CategoryId = CategoryId,
                Introduction = Introduction,
                Content = Content!,
                IsPublished = IsPublished,
                UserId = userId
            };

        public NewPost Merge(NewPost entity)
        {
            entity.Title = Title;
            entity.CategoryId = CategoryId;
            entity.Introduction = Introduction;
            entity.Content = Content!;
            entity.IsPublished = IsPublished;
            return entity;
        }

        public static Expression<Func<NewPost, PostSaveModel>> Selector =>
            bp => new PostSaveModel
            {
                Id = bp.Id,
                Title = bp.Title,
                Slug = bp.Slug,
                CategoryId = bp.CategoryId,
                Introduction = bp.Introduction,
                Content = bp.Content,
                IsPublished = bp.IsPublished
            };
    }
}
