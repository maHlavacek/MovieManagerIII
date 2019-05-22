using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieManager.Core.Contracts;
using MovieManager.Core.DataTransferObjects;

namespace MovieManager.Web.Pages
{
    public class OverViewModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryForOverViewDTO[] CategoryItems { get; private set; }


        public OverViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void OnGet()
        {
            ViewData["Message"] = "Welcome to our streaming service";
            CategoryItems = _unitOfWork.CategoryRepository.GetCategoryForOverViewDTOs().ToArray();
        }
        public IActionResult OnPostCategorySelected(int categoryId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("/CategoryDetails", new { id = categoryId });
        }
    }
}