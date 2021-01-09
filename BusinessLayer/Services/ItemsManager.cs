using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using System;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ItemsManager : IItemsManager
    {
        public Task CreateAsync(CreateItemModel createItemModel)
        {
            throw new NotImplementedException();
        }
    }
}