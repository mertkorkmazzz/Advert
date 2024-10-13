using Advert.Dal.UnitOfWorks;
using Advert.Entity.Entities;
using Advert.Services.Services.Abstractions;
using Advert.Services.Services.Concretae;
using AdvertBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdvertBlog.Controllers
{
	public class HomeController : Controller
	{
		//ANASAYFA / ÝLANLAR LÝSTE ÞEKLÝNDE  / ÝLETÝÞÝM / ADMÝN  PANEL

		private readonly ILogger<HomeController> _logger;
		private readonly IAdvertService advertService;
		private readonly IHttpContextAccessor httpContextAccessor;
		private readonly IUnitOfWork unitOfWork;

		public HomeController(ILogger<HomeController> logger, IAdvertService advertService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
		{
			_logger = logger;
			this.advertService = advertService;
			this.httpContextAccessor = httpContextAccessor;
			this.unitOfWork = unitOfWork;
		}


		public IActionResult Index()
		{
			return View();
		}

		
		[HttpGet]
		public async Task<IActionResult> AdvertsLists(int currentPage = 1, int pageSize = 3, bool isAscending = false)
		{
			
			var adverts = await advertService.GetAllByPagingAsync(null, isAscending);
			return View(adverts); 
		}


		[HttpGet]
		public async Task<IActionResult> Detail(Guid id)
		{
		
			var advert = await advertService.GetArticleWithCategoryNonDeletedAsync(id);

		
			return View(advert);
		}


	}
}

