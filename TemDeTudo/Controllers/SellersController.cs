using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TemDeTudo.Data;
using TemDeTudo.Models;
using TemDeTudo.Models.ViewModels;

namespace TemDeTudo.Controllers
{
    public class SellersController : Controller
    {
        private readonly TemDeTudoContext _context;

        public SellersController(TemDeTudoContext context)
        { 
            _context = context;
        }

        public IActionResult Index()
        {
            var sellers = _context.Seller.Include("Department").ToList();
            return View(sellers);  
        }

        public IActionResult Create()
        {
            //instanciar SellerFormViewModel
            //vai ter as prop vendedor e lista de departamentos 
            SellerFormViewModel viewModel = new SellerFormViewModel();

            //carregando os departamentos do banco de dados
            viewModel.Departments = _context.Department.ToList();

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Create(Seller seller)
        {
           if (seller == null)
            {
                return  NotFound();
            }

            //seller.Department = _context.Department.FirstOrDefault();
            //seller.DepartmentId = seller.Department.Id;


           //Add o objeto vendedor ao banco
           _context.Add(seller);
           _context.SaveChanges();

          return RedirectToAction("Index");
        }
    }
}
