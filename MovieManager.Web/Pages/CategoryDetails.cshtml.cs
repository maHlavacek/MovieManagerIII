using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieManager.Core.Contracts;
using MovieManager.Core.Entities;

namespace MovieManager.Web.Pages
{
    public class CategoryDetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public Movie[] MovieItems { get; private set; }


        public CategoryDetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
            ViewData["Message"] = _unitOfWork.CategoryRepository.GetCategoryById(id).CategoryName;
            MovieItems = _unitOfWork.MovieRepository.GetMoviesByCategoryId(id).ToArray();
        }
    }
}