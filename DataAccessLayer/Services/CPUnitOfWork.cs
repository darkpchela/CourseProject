using DataAccessLayer.Interfaces;
using DataAccessLayer.Services.Repositories;
using System;
using System.Threading.Tasks;

namespace DataAccessLayer.Services
{
    public class CPUnitOfWork : ICPUnitOfWork
    {
        private readonly CPDbContext dbContext;

        private ICollectionItemRepository collectionItemRepository;

        private ICollectionsRepository collectionsRepository;

        private ICommentsRepository commentsRepository;

        private IItemCommentRepository itemCommentRepository;

        private IItemLikeRepository itemLikeRepository;

        private IItemOptionalFieldRepository itemOptionalFieldRepository;

        private IItemsRepository itemsRepository;

        private IItemTagRepository itemTagRepository;

        private IOptionalFieldsRepository optionalFieldsRepository;

        private ITagsRepository tagsRepository;

        private IThemesRepository themesRepository;

        private IUsersRepository usersRepository;

        private IFieldTypesRepository fieldTypesRepository;

        private IResourceRepository resourceRepository;

        public ICollectionItemRepository CollectionItemRepository
        {
            get
            {
                return collectionItemRepository ?? (collectionItemRepository = new CollectionItemRepository(dbContext));
            }
        }

        public ICollectionsRepository CollectionsRepository
        {
            get
            {
                return collectionsRepository ?? (collectionsRepository = new CollectionsRepository(dbContext));
            }
        }

        public ICommentsRepository CommentsRepository
        {
            get
            {
                return commentsRepository ?? (commentsRepository = new CommentsRepository(dbContext));
            }
        }

        public IItemCommentRepository ItemCommentRepository
        {
            get
            {
                return itemCommentRepository ?? (itemCommentRepository = new ItemCommentRepository(dbContext));
            }
        }

        public IItemLikeRepository ItemLikeRepository
        {
            get
            {
                return itemLikeRepository ?? (itemLikeRepository = new ItemLikeRepository(dbContext));
            }
        }

        public IItemOptionalFieldRepository ItemOptionalFieldRepository
        {
            get
            {
                return itemOptionalFieldRepository ?? (itemOptionalFieldRepository = new ItemOptionalFieldRepository(dbContext));
            }
        }

        public IItemsRepository ItemsRepository
        {
            get
            {
                return itemsRepository ?? (itemsRepository = new ItemsRepository(dbContext));
            }
        }

        public IItemTagRepository ItemTagRepository
        {
            get
            {
                return itemTagRepository ?? (itemTagRepository = new ItemTagRepository(dbContext));
            }
        }

        public IOptionalFieldsRepository OptionalFieldsRepository
        {
            get
            {
                return optionalFieldsRepository ?? (optionalFieldsRepository = new OptionalFieldsRepository(dbContext));
            }
        }

        public ITagsRepository TagsRepository
        {
            get
            {
                return tagsRepository ?? (tagsRepository = new TagsRepository(dbContext));
            }
        }

        public IThemesRepository ThemesRepository
        {
            get
            {
                return themesRepository ?? (themesRepository = new ThemesRepository(dbContext));
            }
        }

        public IUsersRepository UsersRepository
        {
            get
            {
                return usersRepository ?? (usersRepository = new UsersRepository(dbContext));
            }
        }

        public IFieldTypesRepository FieldTypesRepository
        {
            get
            {
                return fieldTypesRepository ?? (fieldTypesRepository = new FieldTypesRepository(dbContext));
            }
        }

        public IResourceRepository ResourceRepository
        {
            get
            {
                return resourceRepository ?? (resourceRepository = new ResourceRepository(dbContext));
            }
        }

        public CPUnitOfWork(CPDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        #region Disposable

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
                disposed = true;
            }
        }

        ~CPUnitOfWork()
        {
            Dispose(false);
        }

        #endregion Disposable
    }
}