using System;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces
{
    public interface ICPUnitOfWork : IDisposable
    {
        ICollectionItemRepository CollectionItemRepository { get; }

        ICollectionOptionalFieldRepository CollectionOptionalFieldRepository { get; }

        ICollectionsRepository CollectionsRepository { get; }

        ICommentsRepository CommentsRepository { get; }

        IItemCommentRepository ItemCommentRepository { get; }

        IItemLikeRepository ItemLikeRepository { get; }

        IItemOptionalFieldRepository ItemOptionalFieldRepository { get; }

        IItemsRepository ItemsRepository { get; }

        IItemTagRepository ItemTagRepository { get; }

        IOptionalFieldsRepository OptionalFieldsRepository { get; }

        ITagsRepository TagsRepository { get; }

        IThemesRepository ThemesRepository { get; }

        IUsersRepository UsersRepository { get; }

        IFieldTypesRepository FieldTypesRepository { get; }

        Task SaveChangesAsync();
    }
}