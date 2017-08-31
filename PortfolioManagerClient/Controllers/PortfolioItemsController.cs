using System.Collections.Generic;
using System.Web.Http;
using PortfolioManagerClient.Models;
using Service.Interface;
using Services;
using Service.Interface.Entities;
using PortfolioManagerClient.Infrastructure;
using Services.Services;
using System.Net;
using System;
using System.Threading.Tasks;
using System.Net.Http;

namespace PortfolioManagerClient.Controllers
{
    /// <summary>
    /// Processes portfolio item requests.
    /// </summary>
    public class PortfolioItemsController : ApiController
    {
        private readonly IService<PortfolioItem> _storageService = new StorageService();
        private readonly IUserService _usersService = new UsersService();

        //public PortfolioItemsController(IService<PortfolioItem> storageService,
        //                                                                            IUserService usersService)
        //{
        //    _storageService = storageService;
        //    _usersService = usersService;
        //}

        /// <summary>
        /// Returns all portfolio items for the current user.
        /// </summary>
        /// <returns>The list of portfolio items.</returns>
        public IList<PortfolioItemViewModel> Get()
        {
            var userId = _usersService.GetOrCreateUser();
            return _storageService.GetItems(userId).ToPortfolioItemViewModelList();
        }

        /// <summary>
        /// Updates the existing portfolio item.
        /// </summary>
        /// <param name="portfolioItem">The portfolio item to update.</param>
        public void Put(PortfolioItemViewModel portfolioItem)
        {
            portfolioItem.UserId = _usersService.GetOrCreateUser();
            _storageService.UpdateItem(portfolioItem.ToPortfolioItem());
        }

        /// <summary>
        /// Deletes the specified portfolio item.
        /// </summary>
        /// <param name="id">The portfolio item identifier.</param>
        public void Delete(int id)
        {
            _storageService.DeleteItem(id);
        }

        /// <summary>
        /// Creates a new portfolio item.
        /// </summary>
        /// <param name="portfolioItem">The portfolio item to create.</param>
        public IHttpActionResult Post(PortfolioItemViewModel portfolioItem)
        {
            portfolioItem.UserId = _usersService.GetOrCreateUser();
            try
            {
                _storageService.CreateItem(portfolioItem.ToPortfolioItem());
            }
            catch (HttpRequestException)
            {
                return InternalServerError();
            }
            return Ok();

        }
    }
}
