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

        public IActionResult Details(int? id)
        {   
            //verifica se foi passado um id como parâmetro
            if (id == null)
            {
                return NotFound();
            }
                                                   //x é a variável do vendedor e depois verifica se ele existe; 
            Seller seller = _context.Seller.Include("Department").FirstOrDefault(x => x.Id == id);

            //se não localizar vendedor com id, vai pra página de erro 
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);


        }

        public IActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            Seller  seller = _context.Seller.Include("Department").FirstOrDefault(x =>x.Id == id);
            
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        [HttpPost]
        public IActionResult Delete(int id) 
        {
            if (id == null)
            {
                return NotFound();
            }

            Seller seller = _context.Seller.Include("Department").FirstOrDefault(x => x.Id == id);

            if (seller == null)
            {
                return NotFound();
            }
            _context.Seller.Remove(seller);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id) 
        {
            //Verificar se existe um vendedor com o id passado por parâmetro
            var seller = _context.Seller.First(x => x.Id == id);

            if (seller == null)
            {
                return NotFound();
            }

            //Criar lista de departamentos
            List<Department> departments = _context.Department.ToList();

            //Cria instância do view model 
            SellerFormViewModel viewModel = new SellerFormViewModel();
            viewModel.Seller = seller;
            viewModel.Departments = departments;

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(Seller seller) 
        {
            _context.Update(seller);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }


}
